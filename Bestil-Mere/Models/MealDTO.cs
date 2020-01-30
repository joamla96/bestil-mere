using System.Collections.Generic;

namespace Models
{
    public class MealDTO
    {
        public string Name { get; set; }
        public IEnumerable<MealItemDTO> MealItems { get; set; }
        public IEnumerable<ExtraMealItemDTO> ExtraMealItems { get; set; }
    }
}