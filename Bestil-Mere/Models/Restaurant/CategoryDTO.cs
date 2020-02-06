using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Restaurant
{
    public class CategoryDTO
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public IEnumerable<MealDTO> Meals { get; set; }

        [Required]
        public IEnumerable<ExtraMealItemDTO> ExtraMealItems { get; set; }
    }
}