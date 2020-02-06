using GeoCoordinatePortable;

namespace LogisticsAPI.Models
{
    /// Lots of these fields may not be used for most addresses
    /// but since we're trying to support international stuff
    /// we should handle this in a variety of ways.
    public class Location
    {
        public string StreetName { get; set; }
        public string HouseNo { get; set; }
        public string Apartment { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public GeoCoordinate GeoLocation { get; set; }
    }
}