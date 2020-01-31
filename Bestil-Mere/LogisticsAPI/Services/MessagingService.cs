using EasyNetQ;

namespace LogisticsAPI.Services
{
    public class MessagingService
    {
        public readonly IBus Bus;
        public MessagingService()
        {
            this.Bus = RabbitHutch.CreateBus("host=localhost");
        }
    }
}