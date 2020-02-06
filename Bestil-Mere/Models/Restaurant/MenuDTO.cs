using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Restaurant
{
    public class MenuDTO
    {
        public string Id { get; set; }
        [Required]
        public IEnumerable<CategoryDTO> Categories { get; set; }
    }
}