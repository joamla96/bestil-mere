using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantAPI.Models
{
    public class Menu
    {
        [BsonId]
        public ObjectId Id { get; set; }
        
        public IEnumerable<Meal> Meals { get; set; }
        
    }
}