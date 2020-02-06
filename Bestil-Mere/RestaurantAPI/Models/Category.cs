using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantAPI.Models
{
    public class Category
    {
        public string Name { get; set; }
        
        public IEnumerable<Meal> Meals { get; set; }
        
        public IEnumerable<ExtraMealItem> ExtraMealItems { get; set; }
    }
}