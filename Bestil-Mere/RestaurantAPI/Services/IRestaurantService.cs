using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.Restaurant;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        Task<List<RestaurantDTO>> Get();
        Task<RestaurantDTO> Get(string id);
        Task<RestaurantDTO> Create(CreateRestaurantModel restaurant);
        void Update(UpdateRestaurantModel restaurant);
        void Remove(string id);
        Task AcceptOrder(string orderId);
        Task RejectOrder(string orderId);
        void RequestOrder(RestaurantOrderRequestModel ros);
    }
}