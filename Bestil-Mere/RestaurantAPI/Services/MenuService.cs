using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Restaurant;
using MongoDB.Driver;
using RestaurantAPI.Db;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using RestaurantAPI.Utils.Converters;

namespace MenuAPI.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMongoCollection<Menu> _menus;

        public MenuService(MongoDbManager mgr)
        {
            _menus = mgr.Menus;
        }
        
        public async Task<List<MenuDTO>> Get()
        {
            var findAll = await _menus.FindAsync(x => true);
            var menus = await findAll.ToListAsync(); 
            return menus.Select(r => r.ToMenuDTO()).ToList();
        }

        public async Task<MenuDTO> Get(string id)
        {
            var findAll = await _menus.FindAsync(menu => menu.Id == id);
            var menu = await findAll.FirstOrDefaultAsync();
            return menu.ToMenuDTO();
        }
/*
        public async Task<MenuDTO> Create(CreateMenuModel crm)
        {
            var menu = new Menu()
            {
                Email = crm.Email,
            };
            await _menus.InsertOneAsync(menu);
            return menu.ToMenuDTO();
        }

        public void Update(UpdateMenuModel menuIn) =>
            _menus.ReplaceOne(
                menu => menu.Id == menuIn.Id,
                menuIn.ToMenu());
*/
        public void Remove(string id) => 
            _menus.DeleteOne(menu => menu.Id == id);
    }
}