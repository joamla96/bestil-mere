using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderAPI.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string Country { get; set; } // eg. "DK", "CN", "US" etc..
        public string RestaurantId { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public Order()
        {
            OrderLines = new List<OrderLine>();
        }


    }
}