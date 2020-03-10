using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Models;
using Models.Messages.Logistics;
using Models.Messages.Restaurant;
using Models.Restaurant;
using RestaurantAPI.Models;
using MongoDB.Driver;
using RestaurantAPI.Db;
using RestaurantAPI.Hubs;
using RestaurantAPI.Messaging;
using RestaurantAPI.Utils.Converters;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
       private readonly IMongoCollection<Restaurant> _restaurants;
       private readonly IMenuService _menuService;
       private readonly MessagePublisher _publisher;
       private readonly IHubContext<RestaurantHub> _restaurantHub;
       private readonly RestaurantConnections _restaurantConnections;

        public RestaurantService(
            MongoDbManager mgr, IMenuService menuService, MessagePublisher publisher,
            IHubContext<RestaurantHub> restaurantHub, RestaurantConnections restaurantConnections)
        {
            _restaurants = mgr.Restaurants;
            _menuService = menuService;
            _publisher = publisher;
            _restaurantHub = restaurantHub;
            _restaurantConnections = restaurantConnections;
        }

        public async Task<List<RestaurantDTO>> Get()
        {
            var findAll = await _restaurants.FindAsync(x => true);
            var restaurants = await findAll.ToListAsync(); 
            return restaurants?.Select(r => r.ToRestaurantDTO()).ToList();
        }

        public async Task<RestaurantDTO> Get(string id)
        {
            var findAll = await _restaurants.FindAsync(r => r.Id == id);
            var restaurant = await findAll.FirstOrDefaultAsync();
            return restaurant?.ToRestaurantDTO();
        }

        public async Task<RestaurantDTO> Create(CreateRestaurantModel crm)
        {
            var menu = await _menuService.Create();
            var restaurant = new Restaurant()
            {
                Email = crm.Email,
                RestaurantName = crm.RestaurantName,
                RestaurantType = crm.RestaurantType,
                Cvr = crm.Cvr,
                Address = crm.Address,
                PostalCode = crm.PostalCode,
                City = crm.City,
                Country = crm.Country,
                MenuId = menu.Id
            };
            await _restaurants.InsertOneAsync(restaurant);
            return restaurant.ToRestaurantDTO();
        }
        
        public void Update(UpdateRestaurantModel restaurantIn)
        {
            var update = Builders<Restaurant>.Update
                .Set(r => r.Email, restaurantIn.Email)
                .Set(r => r.RestaurantName, restaurantIn.RestaurantName)
                .Set(r => r.RestaurantType, restaurantIn.RestaurantType)
                .Set(r => r.Cvr, restaurantIn.Cvr)
                .Set(r => r.Address, restaurantIn.Address)
                .Set(r => r.PostalCode, restaurantIn.PostalCode)
                .Set(r => r.City, restaurantIn.City)
                .Set(r => r.Country, restaurantIn.Country);

            _restaurants.UpdateOne(r => r.Id == restaurantIn.Id, update);
        }

        public void Remove(string id) => 
            _restaurants.DeleteOne(restaurant => restaurant.Id == id);

        public async Task AcceptOrder(string orderId)
        {
            await _publisher.PublishDeliveryRequest(new DeliveryRequest()
            {
                OrderId = orderId,
                DeliveryAddress = "",
                PickupTime = DateTime.Now.AddMinutes(new Random().Next(10, 20))
            });

            await _publisher.PublishRestaurantOrderStatus(new RestaurantOrderStatus()
            {
                OrderId = orderId,
                Status = RestaurantOrderStatusDTO.Accepted
            });
        }

        public async Task RejectOrder(string orderId)
        {
            await _publisher.PublishRestaurantOrderStatus(new RestaurantOrderStatus()
            {
                OrderId = orderId,
                Status = RestaurantOrderStatusDTO.Rejected
            });
        }
        
        public void RequestOrder(RestaurantOrderRequestModel ros)
        {
            Console.WriteLine($"Received order {ros.Order.Id}");

            NotifyNewOrder(ros.Order.RestaurantId, ros.Order);
        }
        
        private async void NotifyNewOrder(string restaurantId, OrderDTO order)
        {
            var ci = await _restaurantConnections.GetConnectionIdAsync(restaurantId);
            if (string.IsNullOrEmpty(ci)) return;
            await _restaurantHub.Clients.Client(ci).SendAsync("orderUpdates", order);
        }
    }
}