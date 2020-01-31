using MongoDB.Driver;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class MenuService
    {
        private readonly IMongoCollection<Menu> _menu;

        public MenuService()
        {
            
        }
    }
}