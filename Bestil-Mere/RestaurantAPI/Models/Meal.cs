using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantAPI.Models
{
    public class Meal
    {
        [BsonId]
        public ObjectId Id { get; set; }
        
        public string Name { get; set; }
        
        public string CategoryId { get; set; }

        public IEnumerable<MealItem> MealItem { get; set; }

    }
}