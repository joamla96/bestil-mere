namespace LogisticsAPI.Models
{
    public class LogisticsDatabaseSettings : ILogisticsDatabaseSettings
    {
        public string PartnerCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ILogisticsDatabaseSettings
    {
        string PartnerCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}