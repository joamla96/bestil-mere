using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.Restaurant;

namespace RestaurantAPI.Services
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> Get();
        Task<MenuDTO> Get(string id);
//        Task<MenuDTO> Create(CreateMenuModel menu);
//        void Update(UpdateMenuModel menu);
        void Remove(string id);
    }
}