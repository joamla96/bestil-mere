namespace OrderAPI.Models
{
    public class OrderLine
    {
        public Meal Meal { get; set; }
        public int Quantity { get; set; }
    }
}