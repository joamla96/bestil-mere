using MongoDB.Driver;
using OrderAPI.Models;

namespace OrderAPI.Db
{
    public class MongoDbManager
    {
        public IMongoCollection<Order> Orders { get; }
        public MongoDbManager(IOrderDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Orders = database.GetCollection<Order>(settings.OrdersCollectionName);
        }
    }
}