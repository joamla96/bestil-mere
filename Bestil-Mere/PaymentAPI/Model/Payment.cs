using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaymentAPI.Model
{
    public class Payment
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }
        public PaymentStatus Status { get; set; }
        

        public string Id => ObjectId.ToString();
        public DateTime Created => ObjectId.CreationTime;
    }
}