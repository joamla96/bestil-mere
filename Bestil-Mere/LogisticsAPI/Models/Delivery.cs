using System;

namespace LogisticsAPI.Models
{
    public class Delivery
    {
        public Location LocationFrom { get; set; }
        public Location LocationTo { get; set; }
        public decimal Cost { get; set; }
        public DateTime EstimatedDelivery { get; set; }
    }
}