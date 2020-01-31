namespace PaymentAPI.Db
{
    public class PaymentDatabaseSettings : IPaymentDatabaseSettings
    {
        public string PaymentsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IPaymentDatabaseSettings
    {
        string PaymentsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}