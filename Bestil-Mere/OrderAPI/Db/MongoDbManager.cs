using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
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
            var db = client.GetDatabase(settings.DatabaseName);
            Orders = db.GetCollection<Order>(settings.OrdersCollectionName);
        }
    }
}