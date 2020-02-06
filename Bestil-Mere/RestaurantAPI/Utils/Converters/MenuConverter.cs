using System.Linq;
using Models.Restaurant;
using RestaurantAPI.Models;

namespace RestaurantAPI.Utils.Converters
{
    public static class MenuConverter
    {
        public static MenuDTO ToMenuDTO(this Menu menu)
        {
            return new MenuDTO()
            {
                Id = menu.Id,
                Meals = menu.Meals.Select(m => m.ToMealDTO()).ToList(),
            };
        }
        
        public static MealDTO ToMealDTO(this Meal meal)
        {
            return new MealDTO()
            {
                
                Id = meal.Id,
                Name = meal.Name,
                CategoryId = meal.CategoryId,
                MealItems = meal.MealItems.Select(m => m.ToMealItemDTO()).ToList(),
            };
        }
        
        public static MealItemDTO ToMealItemDTO(this MealItem menu)
        {
            return new MealItemDTO()
            {
                Name = menu.Name
            };
        }
        
    }
}