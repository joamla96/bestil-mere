using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Models.Messages.Payment;
using Models.Messages.Restaurant;
using Models.Order;
using OrderAPI.Services;

namespace OrderAPI.Messaging
{
    public class MessageListener
    {
        private readonly IOrderService _orderService;
        private readonly IMessagingSettings _messagingSettings;

        public MessageListener(IOrderService orderService, IMessagingSettings messagingSettings)
        {
            _orderService = orderService;
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

                // Listen for new payment status'es
                bus.Subscribe<NewPaymentStatus>("order-api",
                    _orderService.OnPaymentStatusUpdate);
                
                // Listen for new order updates from the restaurant
                bus.Subscribe<RestaurantOrderStatus>("order-api",
                    _orderService.OnRestaurantOrderStatus);
                
                Console.WriteLine($"[OrderApi] Register()");
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