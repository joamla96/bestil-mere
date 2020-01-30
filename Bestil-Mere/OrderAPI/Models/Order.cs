using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderAPI.Models
{
    public class Order
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }
        public string CustomerId { get; set; }
        public string RestaurantId { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }

        public string Id => ObjectId.ToString();
        public DateTime Created => ObjectId.CreationTime;

        public Order()
        {
            OrderLines = new List<OrderLine>();
        }


    }
}