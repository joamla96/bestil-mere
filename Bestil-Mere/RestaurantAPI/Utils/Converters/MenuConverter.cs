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
                Categories = menu.Categories.Select(c => c.ToCategoryDTO()).ToList()
            };
        }

        public static CategoryDTO ToCategoryDTO(this Category category)
        {
            return new CategoryDTO()
            {
                Name = category.Name,
                Meals = category.Meals.Select(m => m.ToMealDTO()).ToList(),
                ExtraMealItems = category.ExtraMealItems.Select(emi => emi.ToExtraMealItemDTO()).ToList()
            };
        }
        
        public static MealDTO ToMealDTO(this Meal meal)
        {
            return new MealDTO()
            {
                
                Name = meal.Name,
                Price = meal.Price,
                MealItems = meal.MealItems.Select(m => m.ToMealItemDTO()).ToList(),
            };
        }
        
        public static MealItemDTO ToMealItemDTO(this MealItem mealItem)
        {
            return new MealItemDTO()
            {
                Name = mealItem.Name
            };
        }
        
        public static ExtraMealItemDTO ToExtraMealItemDTO(this ExtraMealItem extraMealItem)
        {
            return new ExtraMealItemDTO()
            {
                Name = extraMealItem.Name,
                Price = extraMealItem.Price
            };
        }
       
        
        public static Menu ToMenu(this UpdateMenuModel menu)
        {
            return new Menu()
            {
                Id = menu.Id,
                Categories = menu.Categories.Select(c => c.ToCategory()).ToList()
            };
        }

        public static Category ToCategory(this CategoryDTO category)
        {
            return new Category()
            {
                Name = category.Name,
                Meals = category.Meals.Select(m => m.ToMeal()).ToList(),
                ExtraMealItems = category.ExtraMealItems.Select(emi => emi.ToExtraMealItem()).ToList()
            };
        }
        
        public static Meal ToMeal(this MealDTO meal)
        {
            return new Meal()
            {
                
                Name = meal.Name,
                Price = meal.Price,
                MealItems = meal.MealItems.Select(m => m.ToMealItem()).ToList(),
            };
        }
        
        public static MealItem ToMealItem(this MealItemDTO mealItem)
        {
            return new MealItem()
            {
                Name = mealItem.Name
            };
        }
        
        public static ExtraMealItem ToExtraMealItem(this ExtraMealItemDTO extraMealItem)
        {
            return new ExtraMealItem()
            {
                Name = extraMealItem.Name,
                Price = extraMealItem.Price
            };
        }
    }
}