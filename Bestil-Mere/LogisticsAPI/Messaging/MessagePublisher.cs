using EasyNetQ;
using Models.Messages.Logistics;

namespace LogisticsAPI.Messaging
{
    public class MessagePublisher
    {
        private IBus _bus;

        public MessagePublisher(IMessagingSettings messagingSettings)
        {
            _bus = RabbitHutch.CreateBus(messagingSettings.ConnectionString);
        }

        public async void PublishNewPartner(NewPartner newPartner)
        {
            await _bus.PublishAsync(newPartner);
        }

        public async void PublishUpdatedPartner(UpdatedPartner updatedPartner)
        {
            await _bus.PublishAsync(updatedPartner);
        }

        public async void PublishDeletedPartner(DeletedPartner deletedPartner)
        {
            await _bus.PublishAsync(deletedPartner);
        }

    }
}