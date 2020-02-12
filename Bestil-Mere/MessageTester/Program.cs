using System;
using System.Threading.Tasks;
using EasyNetQ;
using Models.Payment;

namespace MessageTester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press enter to publish a CreatePaymentModel");
            Console.ReadLine();
            var p = new CreatePaymentModel()
            {
                OrderId = "123456",
                PaymentDetails = new PaymentDetails()
                {
                    CardHolder = "Stefan Olsen",
                    CardNumber = "123456789000",
                    ExpireMonth = 07,
                    ExpireYear = 21,
                    CardSecurityCode = 619
                }
            };
            var bus = RabbitHutch.CreateBus("host=localhost:5672;virtualhost=/;username=admin;password=admin;timeout=120;persistentMessages=false");
            Console.WriteLine($"Created bus..");
            await bus.PublishAsync(p);
            Console.WriteLine($"done!");
            Console.ReadLine();
        }
    }
}