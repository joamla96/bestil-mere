using MongoDB.Driver;
using RestaurantAPI.Models;

namespace RestaurantAPI.Db
{
    public class MongoDbManager
    {
        public IMongoCollection<Restaurant> Restaurants { get; }
        public MongoDbManager(IRestaurantDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Restaurants = database.GetCollection<Restaurant>(settings.RestaurantsCollectionName);
        }
    }
}