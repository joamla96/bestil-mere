using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Models.Restaurant;
using LogisticsAPI.Services;

namespace LogisticsAPI.Messaging
{
    public class MessageListener
    {
        private readonly ILogisticsPartnerService _logisticsService;
        private readonly IMessagingSettings _messagingSettings;

        public MessageListener(ILogisticsPartnerService logisticsService, IMessagingSettings messagingSettings)
        {
            _logisticsService = logisticsService;
            _messagingSettings = messagingSettings;
        }

        /// <summary>
        /// This method will be run during the start up process
        /// </summary>
        public void Register()
        {
            Task.Factory.StartNew(() =>
            {
                using var bus = RabbitHutch.CreateBus(_messagingSettings.ConnectionString);

                // Listen for new logistics requests
//                bus.Subscribe<LogisticsOrderRequestModel>("logistics-api",
//                    _logisticsService.RequestOrder);

                lock (this)
                {
                    Monitor.Wait(this);
                }
            });
        }

        /// <summary>
        /// This method will be run during a graceful shutdown
        /// </summary>
        public void Unregister()
        {
        }
    }
}