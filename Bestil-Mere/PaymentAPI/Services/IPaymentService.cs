using Models.Payment;

namespace PaymentAPI.Services
{
    public interface IPaymentService
    {
        void CreatePayment(CreatePaymentModel model);
    }
}