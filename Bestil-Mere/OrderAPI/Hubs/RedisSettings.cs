namespace OrderAPI.Hubs
{
    public class RedisSettings : IRedisSettings
    {
        public string ConnectionString { get; set; }
        public string OrderConnections { get; set; }
    }

    public interface IRedisSettings
    {
        string ConnectionString { get; set; }
        string OrderConnections { get; set; }
    }
}