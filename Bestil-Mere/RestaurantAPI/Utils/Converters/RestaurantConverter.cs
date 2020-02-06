using Models;
using RestaurantAPI.Models;

namespace RestaurantAPI.Utils.Converters
{
    public static class RestaurantConverter
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
                Country = restaurant.Country,
                MenuId = restaurant.MenuId
            };
        }
        
        public static Restaurant ToRestaurant(this UpdateRestaurantModel restaurant)
        {
            return new Restaurant()
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
    }
}