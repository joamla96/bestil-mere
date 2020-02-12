using GeoCoordinatePortable;
using Models.Logistics;

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

        public LocationDTO Export()
        {
            var model = new LocationDTO()
            {
                StreetName = this.StreetName,
                HouseNo = this.HouseNo,
                Apartment = this.Apartment,
                PostCode = this.PostCode,
                City = this.City,
                State = this.State,
                Country = this.Country
            };

            model.GeoLocation = (this.GeoLocation.Latitude, this.GeoLocation.Longitude);

            return model;
        }

        public static Location Parse(LocationDTO input)
        {
            var model = new Location()
            {
                StreetName = input.StreetName,
                HouseNo = input.HouseNo,
                Apartment = input.Apartment,
                PostCode = input.PostCode,
                City = input.City,
                State = input.State,
                Country = input.Country,
                GeoLocation = new GeoCoordinate(input.GeoLocation.Item1, input.GeoLocation.Item2)
            };

            return model;
        }
    }
}