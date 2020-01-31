using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Models;
using Models.Restaurant;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        Task<List<Restaurant>> Get();
        Task<Restaurant> Get(string id);
        Task<RestaurantDTO> Create(CreateRestaurantModel restaurant);
        void Update(UpdateRestaurantModel restaurant);
        void Remove(string id);
    }
}