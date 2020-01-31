using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.Restaurant;
using RestaurantAPI.Models;
using MongoDB.Driver;
using RestaurantAPI.Db;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
       private readonly IMongoCollection<Restaurant> _restaurants;

        public RestaurantService(MongoDbManager mgr)
        {
            _restaurants = mgr.Restaurants;
        }

        public async Task<List<Restaurant>> Get()
        {
            var findAll = await _restaurants.FindAsync(x => true);
            return await findAll.ToListAsync(); 
        }

        public async Task<Restaurant> Get(string id)
        {
            var findAll = await _restaurants.FindAsync(restaurant => restaurant.Id == id);
            return await findAll.FirstOrDefaultAsync();
        }

        public async Task<RestaurantDTO> Create(CreateRestaurantModel crm)
        {
            var restaurant = new Restaurant()
            {
                Email = crm.Email,
                RestaurantName = crm.RestaurantName,
                RestaurantType = crm.RestaurantType,
                Cvr = crm.Cvr,
                Address = crm.Address,
                PostalCode = crm.PostalCode,
                City = crm.City,
                Country = crm.Country
            };
            await _restaurants.InsertOneAsync(restaurant);
            return restaurant;
        }

        public void Update(string id, Restaurant restaurantIn) =>
            _restaurants.ReplaceOne(restaurant => restaurant.Id.ToString() == id, restaurantIn);

        public void Remove(Restaurant restaurantIn) =>
            _restaurants.DeleteOne(restaurant => restaurant.Id == restaurantIn.Id);

        public void Remove(string id) => 
            _restaurants.DeleteOne(restaurant => restaurant.Id.ToString() == id);
    }
}