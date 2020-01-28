using System.Collections.Generic;
using OrderAPI.Models;

namespace OrderAPI.Services
{
    public interface IOrderService
    {
        List<Order> Get();
        Order Get(string id);
        Order Create(Order order);
        void Update(string id, Order orderIn);
        void Remove(Order orderin);
        void Remove(string id);
    }
}