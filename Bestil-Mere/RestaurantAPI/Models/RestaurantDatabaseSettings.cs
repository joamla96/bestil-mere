namespace RestaurantAPI.Models
{
    public class RestaurantDatabaseSettings : IRestaurantDatabaseSettings
    {
        public string RestaurantsCollectionName { get; set; }
        public string MenusCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IRestaurantDatabaseSettings
    {
        string RestaurantsCollectionName { get; set; }
        string MenusCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}