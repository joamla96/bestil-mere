using System.Collections.Generic;
using Models;
using Models.Order;
using OrderAPI.Models;

namespace OrderAPI.Services
{
    public interface IOrderService
    {
        List<Order> Get();
        Order Get(string id);
        Order Create(CreateOrderModel order);
        void Update(string id, Order orderIn);
        void Remove(Order orderin);
        void Remove(string id);
    }
}