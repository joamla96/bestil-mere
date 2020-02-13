using EasyNetQ;
using LogisticsAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LogisticsAPI.Messaging;
using Models.Messages.Logistics;

namespace LogisticsAPI.Services
{
    public class DeliveryService : IDeliveryService
    {
        private MessagePublisher _publisher;
        private IMongoCollection<Delivery> _deliveries;

        public DeliveryService(MessagePublisher publisher, MongoDbService mongoDbService)
        {
            this._publisher = publisher;
            this._deliveries = mongoDbService.Deliveries;
        }

        public void DeliveryRequest(DeliveryRequest dr)
        {
            Console.WriteLine("DeliveryRequest received...");
            Thread.Sleep(1000);
            var eta = dr.PickupTime.AddMinutes(new Random().Next(10, 20));
            Console.WriteLine("ETA: " + eta.ToString("HH:mm:ss") + " - Sending DeliveryResponse...");
            _publisher.PublishDeliveryResponse(new DeliveryResponse()
            {
                OrderId = dr.OrderId,
                ETA = eta
            });
        }
    }
}
