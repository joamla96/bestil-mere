using EasyNetQ;

namespace LogisticsAPI.Services
{
    public class MessagingService
    {
        public readonly IBus Bus;
        public MessagingService()
        {
            // Todo: Pull this config from the appsettings.json
            this.Bus = RabbitHutch.CreateBus("host=localhost");
        }
    }
}