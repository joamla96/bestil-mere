using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Models.Restaurant;
using RestaurantAPI.Models;
using MongoDB.Driver;
using RestaurantAPI.Db;
using RestaurantAPI.Utils.Converters;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
       private readonly IMongoCollection<Restaurant> _restaurants;

        public RestaurantService(MongoDbManager mgr)
        {
            _restaurants = mgr.Restaurants;
        }

        public async Task<List<RestaurantDTO>> Get()
        {
            var findAll = await _restaurants.FindAsync(x => true);
            var restaurants = await findAll.ToListAsync(); 
            return restaurants.Select(r => r.ToRestaurantDTO()).ToList();
        }

        public async Task<RestaurantDTO> Get(string id)
        {
            var findAll = await _restaurants.FindAsync(restaurant => restaurant.Id == id);
            var restaurant = await findAll.FirstOrDefaultAsync();
            return restaurant.ToRestaurantDTO();
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
            return restaurant.ToRestaurantDTO();
        }

        public void Update(UpdateRestaurantModel restaurantIn) =>
            _restaurants.ReplaceOne(
                restaurant => restaurant.Id == restaurantIn.Id,
                restaurantIn.ToRestaurant());

        public void Remove(string id) => 
            _restaurants.DeleteOne(restaurant => restaurant.Id == id);
    }
}