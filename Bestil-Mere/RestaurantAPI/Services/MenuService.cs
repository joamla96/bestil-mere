using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Restaurant;
using MongoDB.Bson;
using MongoDB.Driver;
using RestaurantAPI.Db;
using RestaurantAPI.Models;
using RestaurantAPI.Utils.Converters;

namespace RestaurantAPI.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMongoCollection<Menu> _menus;

        public MenuService(MongoDbManager mgr)
        {
            _menus = mgr.Menus;
        }

        public async Task<MenuDTO> Get(string id)
        {
            var findAll = await _menus.FindAsync(menu => menu.Id == id);
            var menu = await findAll.FirstOrDefaultAsync();
            return menu.ToMenuDTO();
        }

        public async Task<Menu> Create()
        {
            var menu = new Menu()
            {
                Categories = new List<Category>()
            };
            await _menus.InsertOneAsync(menu);
            return menu;
        }

        public async void Update(UpdateMenuModel menuIn) =>
            await _menus.ReplaceOneAsync(menu => menu.Id == menuIn.Id, menuIn.ToMenu());
        

        public async void Remove(string id) => 
            await _menus.DeleteOneAsync(menu => menu.Id == id);
    }
}