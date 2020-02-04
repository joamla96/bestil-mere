using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantAPI.Models
{
    public class Category
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }
        
        public string Name { get; set; }
        
        public IEnumerable<ExtraMealItem> Extras { get; set; }
        public string Id => ObjectId.ToString();
        public DateTime Created => ObjectId.CreationTime;
    }
}