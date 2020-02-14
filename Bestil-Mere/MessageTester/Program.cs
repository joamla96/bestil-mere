using System;
using System.Threading.Tasks;
using EasyNetQ;
using Models.Messages.Logistics;
using Models.Payment;
using Models.Restaurant;

namespace MessageTester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Please enter message type:\ncpm: CreatePaymentModel\nror: RestaurantOrderRequestModel\ndr: DeliveryRequest");
            var type = Console.ReadLine();

            var bus = RabbitHutch.CreateBus("host=localhost:5672;virtualhost=/;username=admin;password=admin;timeout=120;persistentMessages=false");
            Console.WriteLine($"Created bus..");

            if (type == "cpm")
            {
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
                await bus.PublishAsync(p);
            }

            if (type == "cpm")
            {
                var p = new RestaurantOrderRequestModel()
                {
                    OrderId = "123456",
                };
                await bus.PublishAsync(p);
            }

            if (type == "dr")
            {
                var p = new DeliveryRequest()
                {
                    OrderId = "123456",
                    DeliveryAddress = "Storegade 12, 6700 Esbjerg",
                    PickupTime = DateTime.Now
                };
                await bus.PublishAsync(p);
            }

            Console.WriteLine($"done!");
            Console.ReadLine();
        }
    }
}