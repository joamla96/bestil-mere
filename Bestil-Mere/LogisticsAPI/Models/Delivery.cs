using Models.Logistics;
using System;

namespace LogisticsAPI.Models
{
    public class Delivery
    {
        public Location LocationFrom { get; set; }
        public Location LocationTo { get; set; }
        public decimal Cost { get; set; }
        public DateTime EstimatedDelivery { get; set; }
        public bool Confirmed { get; set; }
        public bool Delivered { get; set; }
        public Partner DeliveryPartner { get; set; }

        public DeliveryDTO Export()
        {
            var model = new DeliveryDTO()
            {
                LocationFrom = this.LocationFrom.Export(),
                LocationTo = this.LocationTo.Export(),
                Cost = this.Cost,
                EstimatedDelivery = this.EstimatedDelivery.ToString(),
                Confirmed = this.Confirmed,
                Delivered = this.Delivered,

            };

            return model;
        }

        public static Delivery Parse(DeliveryDTO input)
        {
            var model = new Delivery()
            {
                LocationFrom = Location.Parse(input.LocationFrom),
                LocationTo = Location.Parse(input.LocationTo),
                Cost = input.Cost,
                EstimatedDelivery = DateTime.Parse(input.EstimatedDelivery),
                Confirmed = input.Confirmed,
                Delivered = input.Delivered
            };

            return model;
        }
    }
}