using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Logistics
{
    public class LocationDTO
    {
        public string StreetName { get; set; }
        public string HouseNo { get; set; }
        public string Apartment { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public (double, double) GeoLocation { get; set; }
    }
}
