using System.Collections.Generic;

namespace Models
{
    public class OrderDTO
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string RestaurantId { get; set; }
        public IEnumerable<OrderLineDTO> OrderLines { get; set; }
    }
}