using System;
using System.Threading;
using System.Threading.Tasks;
using Models.Payment;
using PaymentAPI.Db;
using PaymentAPI.Model;

namespace PaymentAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly MongoDbManager _dbManager;
        public PaymentService(MongoDbManager dbManager)
        {
            _dbManager = dbManager;
        }
        public async void CreatePayment(CreatePaymentModel model)
        {
            Console.WriteLine($"Creating payment..");
            var payment = new Payment()
            {
                Status = PaymentStatus.Created
            };
            await _dbManager.Payments.InsertOneAsync(payment);
        }

        private void AuthorizePayment(Payment p)
        {
            Console.WriteLine($"Authorizing payment...");
            p.Status = PaymentStatus.Authorizing;
            Thread.Sleep(2000);
            Console.WriteLine($"Authorization accepted..");
            p.Status = PaymentStatus.Accepted;
            // Emit to order-api that the payment is OK and continue proceeding the order
        }

        private void UpdatePayment(Payment p)
        {
            // Update payment in db
        }
    }
}