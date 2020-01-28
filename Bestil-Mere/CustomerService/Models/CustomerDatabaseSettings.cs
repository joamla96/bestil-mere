namespace CustomerService.Models
{
    public class CustomerDatabaseSettings : ICustomerDatabaseSettings
    {
        public string CustomersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ICustomerDatabaseSettings
    {
        string CustomersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}