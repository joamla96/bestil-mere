using EasyNetQ;
using LogisticsAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsAPI.Services
{
    public class DeliveryService
    {
        private IBus _bus;
        private IMongoCollection<Delivery> _deliveries;

        public DeliveryService(MessagingService messagingService, MongoDbService mongoDbService)
        {

        }
    }
}
