using Models;
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

    public static class PartnerExtensions
    {
        public static LogisticsPartnerDTO ToDto(this Partner partner)
        {
            var item = new LogisticsPartnerDTO()
            {
                Name = partner.Name
            };

            return item;
        }
    }
}