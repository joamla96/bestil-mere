using System.Collections.Generic;

namespace Models.Restaurant
{
    public class CategoryDTO
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public IEnumerable<ExtraMealItemDTO> ExtraMealItems { get; set; }
    }
}