using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantAPI.Models
{
    public class Menu
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }
        
        public IEnumerable<Meal> Meals { get; set; }
        
        public string Id => ObjectId.ToString();
        public DateTime Created => ObjectId.CreationTime;
    }
}