using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantAPI.Models
{
    public class Meal
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }
        
        public string Name { get; set; }
        
        public string CategoryId { get; set; }

        public IEnumerable<MealItem> MealItems { get; set; }
        
        public string Id => ObjectId.ToString();
        public DateTime Created => ObjectId.CreationTime;

    }
}