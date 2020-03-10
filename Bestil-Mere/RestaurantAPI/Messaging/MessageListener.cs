using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Models.Restaurant;
using RestaurantAPI.Services;

namespace RestaurantAPI.Messaging
{
    public class MessageListener
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IMessagingSettings _messagingSettings;

        public MessageListener(IRestaurantService restaurantService, IMessagingSettings messagingSettings)
        {
            _restaurantService = restaurantService;
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

                // Listen for new restaurant requests
                bus.Subscribe<RestaurantOrderRequestModel>("restaurant-api",
                    _restaurantService.RequestOrder);

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