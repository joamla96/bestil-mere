using System.Threading.Tasks;
using EasyNetQ;
using Models.Messages.Logistics;
using Models.Messages.Restaurant;

namespace RestaurantAPI.Messaging
{
    public class MessagePublisher
    {
        private IBus _bus;

        public MessagePublisher(IMessagingSettings messagingSettings)
        {
            _bus = RabbitHutch.CreateBus(messagingSettings.ConnectionString);
        }

        public async Task PublishRestaurantOrderStatus(RestaurantOrderStatus ros)
        {
            await _bus.PublishAsync(ros);
        }

        public async Task PublishDeliveryRequest(DeliveryRequest dr)
        {
            await _bus.PublishAsync(dr);
        }

    }
}