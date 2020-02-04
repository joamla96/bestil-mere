using System.Collections.Generic;

namespace Models.Restaurant
{
    public class MealDTO
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string CategoryId { get; set; }

        public IEnumerable<MealItemDTO> MealItems { get; set; }

    }
}