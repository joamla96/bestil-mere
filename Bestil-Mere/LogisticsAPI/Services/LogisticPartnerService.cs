using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using LogisticsAPI.Messaging;
using LogisticsAPI.Models;
using Models;
using Models.Messages.Logistics;
using MongoDB.Driver;

namespace LogisticsAPI.Services
{
    public class LogisticPartnerService : ILogisticsPartnerService
    {
        private readonly IMongoCollection<Partner> _partners;
        private readonly MessagePublisher _publisher;

        public LogisticPartnerService(MongoDbService database, MessagePublisher publisher)
        {
            _partners = database.Partners;
            _publisher = publisher;
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
            _publisher.PublishNewPartner(new NewPartner() {Id = partner.Id});
            
            return true;
        }

        public async Task<bool> Insert(IEnumerable<Partner> partners)
        {
            await _partners.InsertManyAsync(partners);
            foreach (var item in partners)
            {
                _publisher.PublishNewPartner(new NewPartner() {Id = item.Id});
            }
            return true;
        }

        public async Task<bool> Update(string id, Partner partner)
        {
            await _partners.ReplaceOneAsync(x => x.Id == id, partner);
            _publisher.PublishUpdatedPartner(new UpdatedPartner() {Id = id});
            return true;
        }

        public async Task<bool> Remove(string id)
        {
            await _partners.DeleteOneAsync(x => x.Id == id);
            _publisher.PublishDeletedPartner(new DeletedPartner() {Id = id});
            return true;
        }

        public Task<bool> Remove(Partner partner)
        {
            return this.Remove(partner.Id);
        }
    }
}