using System.Collections.Generic;

namespace OrderAPI.Models
{
    public class Meal
    {
        public string Name { get; set; }
        public IEnumerable<MealItem> MealItems { get; set; }
        public IEnumerable<ExtraMealItem> ExtraMealItems { get; set; }
    }
}