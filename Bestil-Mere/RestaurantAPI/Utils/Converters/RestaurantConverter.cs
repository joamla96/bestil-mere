using Models;
using RestaurantAPI.Models;

namespace RestaurantAPI.Utils.Converters
{
    public static class OrderConverter
    {
        public static RestaurantDTO ToRestaurantDTO(this Restaurant restaurant)
        {
            return new RestaurantDTO()
            {
                Id = restaurant.Id,
                Email = restaurant.Email,
                RestaurantName = restaurant.RestaurantName,
                RestaurantType = restaurant.RestaurantType,
                Cvr = restaurant.Cvr,
                Address = restaurant.Address,
                PostalCode = restaurant.PostalCode,
                City = restaurant.City,
                Country = restaurant.Country
            };
        }
        
        public static Restaurant ToRestaurant(this UpdateRestaurantModel restaurant)
        {
            return new Restaurant()
            {
                Email = restaurant.Email,
                RestaurantName = restaurant.RestaurantName,
                RestaurantType = restaurant.RestaurantType,
                Cvr = restaurant.Cvr,
                Address = restaurant.Address,
                PostalCode = restaurant.PostalCode,
                City = restaurant.City,
                Country = restaurant.Country
            };
        }
    }
}