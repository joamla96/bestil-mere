using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.Messages.Payment;
using Models.Messages.Restaurant;
using Models.Order;
using OrderAPI.Models;

namespace OrderAPI.Services
{
    public interface IOrderService
    {
        Task<List<Order>> Get();
        Task<Order> Get(string id);
        Task<List<Order>> GetByCustId(string customerId);
        Task<Order> Create(CreateOrderModel order);
        void Remove(string id);
        void OnPaymentStatusUpdate(NewPaymentStatus status);
        void OnRestaurantOrderStatus(RestaurantOrderStatus restaurantOrderStatus);
    }
}