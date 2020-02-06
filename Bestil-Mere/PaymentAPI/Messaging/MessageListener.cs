using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Models.Payment;
using PaymentAPI.Services;

namespace PaymentAPI.Messaging
{
    public class MessageListener
    {
        private readonly IPaymentService _paymentService;
        private readonly IMessagingSettings _messagingSettings;

        public MessageListener(IPaymentService paymentService, IMessagingSettings messagingSettings)
        {
            _paymentService = paymentService;
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

                // Listen for new payment requests
                bus.Subscribe<CreatePaymentModel>("payment-api",
                    _paymentService.CreatePayment);

                // Block the thread so that it will not exit and stop subscribing.
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