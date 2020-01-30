using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Order>> Get()
        {
            var findAll = await _orders.FindAsync(x => true);
            return await findAll.ToListAsync(); 
        }

        public async Task<Order> Get(string id)
        {
            var findAll = await _orders.FindAsync(order => order.Id == id);
            return await findAll.FirstOrDefaultAsync();

        }

        public async Task<Order> Create(CreateOrderModel createOrderModel)
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
            await _orders.InsertOneAsync(order);
            return order;
        }

        public void Update(string id, Order orderIn) =>
            _orders.ReplaceOne(order => order.Id == id, orderIn);

        public void Remove(Order orderIn) =>
            _orders.DeleteOne(order => order.Id == orderIn.Id);

        public void Remove(string id) => 
            _orders.DeleteOneAsync(order => order.Id == id);
    }
}