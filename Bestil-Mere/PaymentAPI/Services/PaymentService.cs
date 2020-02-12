using System;
using System.Threading;
using System.Threading.Tasks;
using Models.Messages.Payment;
using Models.Payment;
using MongoDB.Driver;
using PaymentAPI.Db;
using PaymentAPI.Messaging;
using PaymentAPI.Model;

namespace PaymentAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMongoCollection<Payment> _payments;
        private readonly MessagePublisher _publisher;
        public PaymentService(MongoDbManager dbManager, MessagePublisher publisher)
        {
            _payments = dbManager.Payments;
            _publisher = publisher;
        }
        
        public async void CreatePayment(CreatePaymentModel model)
        {
            Console.WriteLine($"[Creating new Payment] for order: {model.OrderId}");
            var payment = new Payment()
            {
                Status = PaymentStatus.Created,
                OrderId = model.OrderId
            };
            await _payments.InsertOneAsync(payment);
            
            AuthorizePayment(payment);
        }

        private async void AuthorizePayment(Payment p)
        {
            p.Status = PaymentStatus.Authorizing;
            await UpdateAndPublish(p);
            
            // TODO: What should we do here?
            Thread.Sleep(2000);
            
            p.Status = PaymentStatus.Accepted;
            await UpdateAndPublish(p);
        }
        
        public async Task<Payment> Get(string id)
        {
            var findAll = await _payments.FindAsync(order => order.Id == id);
            return await findAll.FirstOrDefaultAsync();
        }

        private async Task UpdateAndPublish(Payment p)
        {
            var up = Builders<Payment>.Update
                .Set(pp => pp.Status, p.Status);

            await _payments.UpdateOneAsync(u => u.Id == p.Id, up);
            _publisher.PublishNewPaymentStatus(new NewPaymentStatus()
            {
                Status = p.Status.Parse(),
                OrderId = p.OrderId,
                PaymentId = p.Id
            });
        }
    }
}