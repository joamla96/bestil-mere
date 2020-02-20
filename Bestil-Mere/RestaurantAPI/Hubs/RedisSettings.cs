namespace RestaurantAPI.Hubs
{
    public class RedisSettings : IRedisSettings
    {
        public string ConnectionString { get; set; }
        public string RestaurantConnections { get; set; }
    }

    public interface IRedisSettings
    {
        string ConnectionString { get; set; }
        string RestaurantConnections { get; set; }
    }
}