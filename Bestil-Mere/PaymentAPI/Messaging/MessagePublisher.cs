using EasyNetQ;
using Models.Messages.Payment;
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

        public async void PublishNewPaymentStatus(NewPaymentStatus nps)
        {
            await _bus.PublishAsync(nps);
        }

    }
}