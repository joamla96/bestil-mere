using LogisticsAPI.Models;
using MongoDB.Driver;

namespace LogisticsAPI.Services
{
    public class MongoDbService
    {
        internal readonly IMongoCollection<Partner> Partners;
        internal readonly IMongoCollection<Delivery> Deliveries;
        
        public MongoDbService(ILogisticsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Partners = database.GetCollection<Partner>(settings.PartnerCollectionName);
            Deliveries = database.GetCollection<Delivery>(settings.DeliveryCollectionName);
        }
    }
}