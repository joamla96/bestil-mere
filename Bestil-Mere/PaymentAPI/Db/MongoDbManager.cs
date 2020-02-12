using MongoDB.Driver;
using PaymentAPI.Model;

namespace PaymentAPI.Db
{
    public class MongoDbManager
    {
        public IMongoCollection<Payment> Payments { get; }
        public MongoDbManager(IPaymentDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Payments = database.GetCollection<Payment>(settings.PaymentsCollectionName);
        }
    }
}