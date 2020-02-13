using EasyNetQ;
using LogisticsAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsAPI.Messaging;

namespace LogisticsAPI.Services
{
    public class DeliveryService
    {
        private MessagePublisher _publisher;
        private IMongoCollection<Delivery> _deliveries;

        public DeliveryService(MessagePublisher publisher, MongoDbService mongoDbService)
        {
            this._publisher = publisher;
            this._deliveries = mongoDbService.Deliveries;
        }
    }
}
