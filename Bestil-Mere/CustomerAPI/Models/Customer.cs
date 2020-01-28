using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CustomerAPI.Models
{
    public class Customergit 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }
        
        public string Country { get; set; }
    }
}