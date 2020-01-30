namespace Models.Order
{
    public class CreateOrderLineModel
    {
        public CreateMealModel Meal { get; set; }
        public int Quantity { get; set; }
    }
}