using System.Collections.Generic;
using System.Linq;
using Models;
using Models.Order;
using MongoDB.Driver;
using OrderAPI.Db;
using OrderAPI.Models;
using ExtraMealItem = OrderAPI.Models.ExtraMealItem;
using MealItem = OrderAPI.Models.MealItem;

namespace OrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<Order> _orders;
        public OrderService(MongoDbManager mgr)
        {
            _orders = mgr.Orders;
        }

        public List<Order> Get() =>
            _orders.Find(order => true).ToList();

        public Order Get(string id) =>
            _orders.Find<Order>(order => order.Id == id).FirstOrDefault();

        public Order Create(CreateOrderModel createOrderModel)
        {
            var order = new Order()
            {
                CustomerId = createOrderModel.CustomerId,
                RestaurantId = createOrderModel.RestaurantId,
                OrderLines = createOrderModel.OrderLines.Select(x => new OrderLine()
                {
                    Quantity = x.Quantity,
                    Meal = new Meal()
                    {
                        Name = x.Meal.Name,
                        MealItems = x.Meal.MealItems.Select(m => new MealItem()
                        {
                            Name = m.Name
                        }),
                        ExtraMealItems = x.Meal.ExtraMealItems.Select(em => new ExtraMealItem()
                        {
                            Name = em.Name,
                            Quantity = em.Quantity
                        })
                    }
                })
            };
            _orders.InsertOne(order);
            return order;
        }

        public void Update(string id, Order orderIn) =>
            _orders.ReplaceOne(order => order.Id == id, orderIn);

        public void Remove(Order orderIn) =>
            _orders.DeleteOne(order => order.Id == orderIn.Id);

        public void Remove(string id) => 
            _orders.DeleteOne(order => order.Id == id);
    }
}