using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RestaurantAPI.Hubs
{
    public class RestaurantConnections
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly string _prefix;
        
        public RestaurantConnections(IRedisSettings settings)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(settings.ConnectionString);
            _prefix = settings.RestaurantConnections;
        }

        /// <summary>
        /// Get the client's connection-id by the restaurant-id
        /// Returns null if the restaurant-id does not exist in redis
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public async Task<string> GetConnectionIdAsync(string restaurantId)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var c = await db.StringGetAsync($"{_prefix}-{restaurantId}");
            return c.IsNullOrEmpty ? null : c.ToString();
        }

        /// <summary>
        /// Sets the restaurant-id as key, and the connection-id as value
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public async Task<bool> SetConnectionIdAsync(string restaurantId, string cid)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.StringSetAsync($"{_prefix}-{restaurantId}", cid);
        }

        public bool SetConnectionId(string restaurantId, string cid)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return db.StringSet($"{_prefix}-{restaurantId}", cid);
        }

        /// <summary>
        /// Removes the key-value with the restaurant-id as key
        /// Does nothing if it does not exist
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string restaurantId)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.KeyDeleteAsync($"{_prefix}-{restaurantId}");
        }
    }
}