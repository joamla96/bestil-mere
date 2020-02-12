namespace PaymentAPI.Messaging
{
    public class MessagingSettings : IMessagingSettings
    {
        public string ConnectionString { get; set; }
    }
    
    public interface IMessagingSettings
    {
        string ConnectionString { get; set; }
    }
}