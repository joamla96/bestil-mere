using EasyNetQ;
using LogisticsAPI.Models;

namespace LogisticsAPI.Services
{
    public class MessagingService
    {
        public readonly IBus Bus;
        public MessagingService(MessagingSettings settings)
        {
            this.Bus = RabbitHutch.CreateBus(settings.ConnectionString);
        }
        
        
    }
}