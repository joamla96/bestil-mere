using System.Collections.Generic;

namespace Models.Restaurant
{
    public class MealDTO
    {
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public IEnumerable<MealItemDTO> MealItems { get; set; }

    }
}