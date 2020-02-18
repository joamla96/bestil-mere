using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Models;
using Models.Messages.Restaurant;
using Models.Restaurant;
using RestaurantAPI.Models;
using MongoDB.Driver;
using RestaurantAPI.Db;
using RestaurantAPI.Messaging;
using RestaurantAPI.Utils.Converters;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
       private readonly IMongoCollection<Restaurant> _restaurants;
       private readonly IMenuService _menuService;
       private readonly MessagePublisher _publisher;

        public RestaurantService(MongoDbManager mgr, IMenuService menuService, MessagePublisher publisher)
        {
            _restaurants = mgr.Restaurants;
            _menuService = menuService;
            _publisher = publisher;
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

        public void RequestOrder(RestaurantOrderRequestModel ros)
        {
            Console.WriteLine("[Restaurant Order Request] Waiting for response..");
            Thread.Sleep(5000);
            
            Console.WriteLine($"[Restaurant Order Request] Order request accepted");
            _publisher.PublishRestaurantOrderStatus(new RestaurantOrderStatus()
            {
                OrderId = ros.Order.Id,
                Status = RestaurantOrderStatusDTO.Accepted
            });
        }
    }
}