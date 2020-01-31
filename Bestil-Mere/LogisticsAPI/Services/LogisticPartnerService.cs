using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using LogisticsAPI.Models;
using Models;
using Models.Messages.Logistics;
using MongoDB.Driver;

namespace LogisticsAPI.Services
{
    public class LogisticPartnerService : ILogisticsPartnerService
    {
        private readonly IMongoCollection<Partner> _partners;
        private readonly IBus _bus;

        public LogisticPartnerService(MongoDbService database, MessagingService msgService)
        {
            this._partners = database.partners;
            this._bus = msgService.Bus;
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
            await _bus.PublishAsync(new NewPartner() {Id = partner.Id});
            
            return true;
        }

        public async Task<bool> Insert(IEnumerable<Partner> partners)
        {
            await _partners.InsertManyAsync(partners);
            foreach (var item in partners)
            {
                await _bus.PublishAsync(new NewPartner() {Id = item.Id});
            }
            return true;
        }

        public async Task<bool> Update(string id, Partner partner)
        {
            await _bus.PublishAsync(new UpdatedPartner() {Id = id});
            await _partners.ReplaceOneAsync(x => x.Id == id, partner);
            return true;
        }

        public async Task<bool> Remove(string id)
        {
            await _bus.PublishAsync(new DeletedPartner() {Id = id});
            await _partners.DeleteOneAsync(x => x.Id == id);
            return true;
        }

        public Task<bool> Remove(Partner partner)
        {
            return this.Remove(partner.Id);
        }
    }
}