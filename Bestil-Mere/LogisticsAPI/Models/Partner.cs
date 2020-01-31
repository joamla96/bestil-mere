using MongoDB.Driver;
using MongoDB.Bson;

namespace LogisticsAPI.Models
{
    public class Partner
    {
        public ObjectId ObjectId {get;set;}
    
        public string Id => this.ObjectId.ToString(); 
        public string Name { get; set; }
    }
}