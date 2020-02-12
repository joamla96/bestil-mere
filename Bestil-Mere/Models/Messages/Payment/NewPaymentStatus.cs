using Models.Payment;

namespace Models.Messages.Payment
{
    public class NewPaymentStatus
    {
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public PaymentStatusDTO Status { get; set; }
    }
}