using System.Linq;
using Models;
using OrderAPI.Models;

namespace OrderAPI.Utils.Converters
{
    public static class OrderConverter
    {
        public static OrderDTO ToOrderDTO(this Order order)
        {
            return new OrderDTO()
            {
                Id = order.Id,
                Country = order.Country,
                CustomerId = order.CustomerId,
                RestaurantId = order.RestaurantId,
                OrderLines = order.OrderLines.Select(x => new OrderLineDTO()
                {
                    Meal = new MealDTO()
                    {
                        Name = x.Meal.Name,
                        MealItems = x.Meal.MealItems.Select(mi => new MealItemDTO()
                        {
                            Name = mi.Name
                        }).ToList(),
                        ExtraMealItems = x.Meal.ExtraMealItems.Select(emi => new ExtraMealItemDTO()
                        {
                            Name = emi.Name,
                            Quantity = emi.Quantity
                        }).ToList()
                    },
                    Quantity = x.Quantity
                }).ToList()
            };
        }
    }
}