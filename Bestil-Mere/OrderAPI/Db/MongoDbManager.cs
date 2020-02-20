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
        private readonly IMongoDatabase _db;
        private readonly MongoClient _client;

        public MongoDbManager(IOrderDatabaseSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            _db = _client.GetDatabase(settings.DatabaseName);
            Orders = _db.GetCollection<Order>(settings.OrdersCollectionName);
        }

        public async Task InitShardingOnOrders()
        {
            // Enable sharding on OrderDb
            // sh.enableSharding("OrderDb")
            //
            // Shard the Orders collection
            // sh.shardCollection("OrderDb.Orders", {Country: 1, CustomerId: 1}) // Very important that the cust-id is relative to the country
            //
            // Disable balancing while configuring
            // sh.disableBalancing("OrderDb.Orders")
            //
            // Add shard zones
            // sh.addShardToZone("shard01", "EU")
            // sh.addShardToZone("shard02", "NA")
            // sh.addShardToZone("shard03", "AS")
            //
            // sh.addTagRange("OrderDb.Orders", {"Country": "DK", "CustomerId": MinKey}, {"Country": "DK", "CustomerId": MaxKey}, "EU")
            // ... .. ...
            //
            // Enable balancing again here
            // sh.enableBalancing("OrderDb.Orders")
            //

            // OrderDb
            var database = Orders.Database;
            
            // admin and config db
            var adminDb = _client.GetDatabase("admin");
            var configDb = _client.GetDatabase("config");
            
            var databaseName = database.DatabaseNamespace.DatabaseName;
            
            // Enable sharding on the db
            adminDb.RunCommand<BsonDocument>(new BsonDocument()
            {
                {"enableSharding", $"{databaseName}"}
            });
            
            // Shard the Orders collection
            var partition = new BsonDocument
            {
                {
                    "shardCollection",
                    $"{_db.DatabaseNamespace.DatabaseName}.{Orders.CollectionNamespace.CollectionName}"
                },
                {"key", new BsonDocument {{"Country", "1"}, {"CustomerId", "1"}}}
            };
            var command = new BsonDocumentCommand<BsonDocument>(partition);
            await _db.RunCommandAsync(command);
        }

        public async Task EnableZoneShards()
        {
            // Enable the zone here
        }
    }
}