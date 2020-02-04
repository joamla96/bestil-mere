using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantAPI.Models
{
    public class Restaurant
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }

        public string Email { get; set; }

        public string RestaurantName { get; set; }
       
        public string RestaurantType { get; set; }
        
        public string Cvr { get; set; }
        
        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }
        
        public string Country { get; set; }
        
        public string Id => ObjectId.ToString();
        public DateTime Created => ObjectId.CreationTime;
    }
}