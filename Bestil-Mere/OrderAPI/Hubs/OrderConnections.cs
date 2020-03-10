using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace OrderAPI.Hubs
{
    public class OrderConnections
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly string _prefix;
        
        public OrderConnections(IRedisSettings settings)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(settings.ConnectionString);
            _prefix = settings.OrderConnections;
        }

        /// <summary>
        /// Get the client's connection-id by the order-id
        /// Returns null if the order-id does not exist in redis
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<string> GetConnectionIdAsync(string orderId)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var c = await db.StringGetAsync($"{_prefix}-{orderId}");
            return c.IsNullOrEmpty ? null : c.ToString();
        }

        /// <summary>
        /// Sets the order-id as key, and the connection-id as value
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public async Task<bool> SetConnectionIdAsync(string orderId, string cid)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.StringSetAsync($"{_prefix}-{orderId}", cid);
        }

        public bool SetConnectionId(string orderId, string cid)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return db.StringSet($"{_prefix}-{orderId}", cid);
        }

        /// <summary>
        /// Removes the key-value with the order-id as key
        /// Does nothing if it does not exist
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string orderId)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.KeyDeleteAsync($"{_prefix}-{orderId}");
        }
    }
}