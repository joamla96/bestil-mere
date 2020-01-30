using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Order
{
    public class CreateOrderModel
    {
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string RestaurantId { get; set; }
        [Required]
        public IEnumerable<CreateOrderLineModel> OrderLines { get; set; }
    }
}