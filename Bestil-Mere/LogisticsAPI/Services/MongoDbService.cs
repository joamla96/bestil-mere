using LogisticsAPI.Models;
using MongoDB.Driver;

namespace LogisticsAPI.Services
{
    public class MongoDbService
    {
        internal readonly IMongoCollection<Partner> partners;
        internal readonly IMongoCollection<Delivery> deliveries;
        
        public MongoDbService(ILogisticsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            partners = database.GetCollection<Partner>(settings.PartnerCollectionName);
            deliveries = database.GetCollection<Delivery>(settings.DeliveryCollectionName);
        }
    }
}