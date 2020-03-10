using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Models;
using Models.Payment;
using Models.Restaurant;
using OrderAPI.Models;
using OrderAPI.Utils.Converters;
using ExtraMealItemDTO = Models.ExtraMealItemDTO;
using MealDTO = Models.MealDTO;
using MealItemDTO = Models.MealItemDTO;

namespace OrderAPI.Messaging
{
    public class MessagePublisher
    {
        private IBus _bus;

        public MessagePublisher(IMessagingSettings messagingSettings)
        {
            _bus = RabbitHutch.CreateBus(messagingSettings.ConnectionString);
        }

        /// <summary>
        /// Authorize a payment when an order has been created by a customer
        /// Publishes a CreatePaymentModel to the broker
        /// TODO: Implement payment details
        /// </summary>
        /// <param name="o"></param>
        public async Task AuthorizePaymentForOrder(Order o)
        {
            var pm = new CreatePaymentModel
            {
                OrderId = o.Id,
                PaymentDetails =
                    null, // TODO: Include paymentDetails when the order is created to parse on to paymentAPI
            };
            Console.WriteLine($"[Create Payment] Order id: {pm.OrderId}");
            await _bus.PublishAsync(pm);
        }

        /// <summary>
        /// When the payment has been accepted, ask the restaurant to accept/reject the new order
        /// Publishes a new RestaurantOrderRequestModel to the broker
        /// </summary>
        /// <param name="o"></param>
        public async Task NewOrderRequest(Order o)
        {
            var pm = new RestaurantOrderRequestModel
            {
                Order = o.ToOrderDTO()
            };
            await _bus.PublishAsync(pm);
            Console.WriteLine($"[Create New Order request to restaurant] Order id: {pm.Order.Id}");
        }
    }
}