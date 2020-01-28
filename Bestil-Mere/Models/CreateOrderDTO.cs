using System.Collections.Generic;

namespace Models
{
    public class CreateOrderDTO
    {
        public string CustomerId { get; set; }
        public string RestaurantId { get; set; }
        public IEnumerable<OrderLineDTO> OrderLines { get; set; }
    }
}