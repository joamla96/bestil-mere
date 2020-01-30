using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsAPI.Models;
using Models;
using MongoDB.Driver;

namespace LogisticsAPI.Services
{
    public class LogisticPartnerService
    {
        private readonly IMongoCollection<Partner> _partners;
        public LogisticPartnerService(ILogisticsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _partners = database.GetCollection<Partner>(settings.PartnerCollectionName);
        }

        public async Task<ICollection<Partner>> Get()
        {
            var findAll = await _partners.FindAsync(x => true);
            return await findAll.ToListAsync();
        }

        public async Task<Partner> Get(string id)
        {
            var findItem = await _partners.FindAsync(x => x.Id == id);
            return await findItem.FirstOrDefaultAsync();
        }

        public async Task<bool> Insert(Partner partner)
        {
            await _partners.InsertOneAsync(partner);
            return true;
        }

        public async Task<bool> Insert(IEnumerable<Partner> partners)
        {
            await _partners.InsertManyAsync(partners);
            return true;
        }

        public Task<bool> Insert(CreateLogisticsPartnerDTO partnerDto)
        {
            var partner = new Partner()
            {
                Name = partnerDto.Name
            };
            return this.Insert(partner);
        }

        public async Task<bool> Update(string id, Partner partner)
        {
            await _partners.ReplaceOneAsync(x => x.Id == id, partner);
            return true;
        }

        public async Task<bool> Remove(string id)
        {
            await _partners.DeleteOneAsync(x => x.Id == id);
            return true;
        }

        public Task<bool> Remove(Partner partner)
        {
            return this.Remove(partner.Id);
        }
    }
}