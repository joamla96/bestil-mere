using System.Collections.Generic;
using RestaurantAPI.Models;
using MongoDB.Driver;

namespace RestaurantAPI.Services
{
    public class RestaurantService
    {
        private readonly IMongoCollection<Restaurant> _restaurants;

        public RestaurantService(IRestaurantDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _restaurants = database.GetCollection<Restaurant>(settings.RestaurantsCollectionName);
        }

        public List<Restaurant> Get() =>
            _restaurants.Find(restaurant => true).ToList();

        public Restaurant Get(string id) =>
            _restaurants.Find<Restaurant>(restaurant => restaurant.Id == id).FirstOrDefault();

        public Restaurant Create(Restaurant restaurant)
        {
            _restaurants.InsertOne(restaurant);
            return restaurant;
        }

        public void Update(string id, Restaurant restaurantIn) =>
            _restaurants.ReplaceOne(restaurant => restaurant.Id == id, restaurantIn);

        public void Remove(Restaurant restaurantIn) =>
            _restaurants.DeleteOne(restaurant => restaurant.Id == restaurantIn.Id);

        public void Remove(string id) => 
            _restaurants.DeleteOne(restaurant => restaurant.Id == id);
    }
}