using System.Linq;
using Models;
using RestaurantAPI.Models;

namespace OrderAPI.Utils.Converters
{
    public static class OrderConverter
    {
        public static RestaurantDTO ToRestaurantDTO(this Restaurant restaurant)
        {
            return new RestaurantDTO()
            {
                Id = order.Id,
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
                        }),
                        ExtraMealItems = x.Meal.ExtraMealItems.Select(emi => new ExtraMealItemDTO()
                        {
                            Name = emi.Name,
                            Quantity = emi.Quantity
                        })
                    },
                    Quantity = x.Quantity
                })
            };
        }
    }
}