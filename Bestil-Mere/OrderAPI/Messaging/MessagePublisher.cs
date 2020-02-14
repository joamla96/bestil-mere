using System;
using EasyNetQ;
using Models.Messages.Payment;
using Models.Payment;
using OrderAPI.Models;

namespace OrderAPI.Messaging
{
    public class MessagePublisher
    {
        private IBus _bus;

        public MessagePublisher(IMessagingSettings messagingSettings)
        {
            _bus = RabbitHutch.CreateBus(messagingSettings.ConnectionString);
        }

        public async void AuthorizePaymentForOrder(Order o)
        {
            var pm = new CreatePaymentModel
            {
                OrderId = o.Id,
                PaymentDetails = null, // TODO: Include paymentDetails when the order is created to parse on to paymentAPI
            };
            Console.WriteLine($"[Create Payment] Order id: {pm.OrderId}");
            await _bus.PublishAsync(pm);
        }
        

    }
}