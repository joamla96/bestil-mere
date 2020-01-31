using LogisticsAPI.Models;
using MongoDB.Driver;

namespace LogisticsAPI.Services
{
    public class MongoDbService
    {
        internal readonly IMongoCollection<Partner> partners;
        
        public MongoDbService(ILogisticsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            partners = database.GetCollection<Partner>(settings.PartnerCollectionName);
        }
    }
}