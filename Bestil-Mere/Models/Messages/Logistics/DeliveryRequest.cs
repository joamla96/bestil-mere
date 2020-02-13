using System;

namespace Models.Messages.Logistics
{
    public class DeliveryRequest
    {
        public string OrderId { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime PickupTime { get; set; }
    }
}