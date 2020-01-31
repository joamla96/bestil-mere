namespace Models.Payment
{
    public class CreatePaymentModel
    {
        public string OrderId { get; set; }
        public PaymentDetails PaymentDetails { get; set; }
    }
}