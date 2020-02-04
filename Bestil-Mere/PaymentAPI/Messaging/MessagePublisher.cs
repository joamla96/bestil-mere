using EasyNetQ;
using PaymentAPI.Model;

namespace PaymentAPI.Messaging
{
    public class MessagePublisher
    {
        private IBus _bus;

        public MessagePublisher(IMessagingSettings messagingSettings)
        {
            _bus = RabbitHutch.CreateBus(messagingSettings.ConnectionString);
        }

        public void PaymentStatusChanged(Payment p)
        {
            // Emit whenever the status changing on the Payment
        }
    }
}