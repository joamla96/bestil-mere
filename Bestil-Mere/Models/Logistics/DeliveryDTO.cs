using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Logistics
{
    public class DeliveryDTO
    {
        public LocationDTO LocationFrom { get; set; }
        public LocationDTO LocationTo { get; set; }
        public decimal Cost { get; set; }
        public string EstimatedDelivery { get; set; }
        public bool Confirmed { get; set; }
        public bool Delivered { get; set; }
        //public PartnerDTO DeliveryPartner { get; set; } // TODO: PartnerDTO
    }
}
