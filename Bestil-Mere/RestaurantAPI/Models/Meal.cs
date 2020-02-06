using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models
{
    public class Meal
    {
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public IEnumerable<MealItem> MealItems { get; set; }
    }
}