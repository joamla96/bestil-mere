using Models.Restaurant;

namespace Models.Messages.Restaurant
{
    public class RestaurantOrderStatus
    {
        public string OrderId { get; set; }
        public RestaurantOrderStatusDTO Status { get; set; }
    }
}