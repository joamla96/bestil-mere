namespace Models
{
    public class OrderLineDTO
    {
        public string OriginalMealId { get; set; }
        public MealDTO Meal { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}