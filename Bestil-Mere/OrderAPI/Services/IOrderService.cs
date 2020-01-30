using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.Order;
using OrderAPI.Models;

namespace OrderAPI.Services
{
    public interface IOrderService
    {
        Task<List<Order>> Get();
        Task<Order> Get(string id);
        Task<Order> Create(CreateOrderModel order);
        void Remove(string id);
    }
}