namespace Models.Payment
{
    public class PaymentDetails
    {
        public string CardNumber { get; set; }
        public string CardHolder { get; set; } // Name of the person with the card
        public string CardSecurityCode { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
        
    }
}