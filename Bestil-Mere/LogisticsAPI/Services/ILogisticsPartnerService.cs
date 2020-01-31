using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogisticsAPI.Models;

namespace LogisticsAPI.Services
{
    public interface ILogisticsPartnerService
    {
        Task<ICollection<Partner>> Get();
        Task<Partner> Get(string id);
        Task<bool> Insert(Partner partner);
        Task<bool> Update(string id, Partner partner);
        Task<bool> Remove(string id);
    }
}