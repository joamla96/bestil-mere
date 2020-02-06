using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.Restaurant;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IMenuService
    {
        Task<MenuDTO> Get(string id);
        Task<Menu> Create();
        void Update(UpdateMenuModel menu);
        void Remove(string id);
    }
}