using System.Collections.Generic;

namespace Models.Restaurant
{
    public class MenuDTO
    {
        public string Id { get; set; }
        
        public IEnumerable<MealDTO> Meals { get; set; }
    }
}