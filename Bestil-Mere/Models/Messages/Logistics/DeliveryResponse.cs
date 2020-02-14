using System;

namespace Models.Messages.Logistics
{
    public class DeliveryResponse
    {
        public string OrderId { get; set; }
        public DateTime ETA { get; set; }
    }
}