using System.Collections.Generic;
using RestaurantAPI.Models;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : Controller
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantsController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        
        [HttpGet]
        public ActionResult<List<Restaurant>> Get() =>
            _restaurantService.Get();

        [HttpGet("{id:length(24)}", Name = "GetRestaurant")]
        public ActionResult<Restaurant> Get(string id)
        {
            var restaurant = _restaurantService.Get(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }

        [HttpPost]
        public ActionResult<Restaurant> Create(Restaurant restaurant)
        {
            _restaurantService.Create(restaurant);

            return CreatedAtRoute("GetRestaurant", new { id = restaurant.Id.ToString() }, restaurant);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Restaurant restaurantIn)
        {
            var restaurant = _restaurantService.Get(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            _restaurantService.Update(id, restaurantIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var restaurant = _restaurantService.Get(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            _restaurantService.Remove(restaurant.Id);

            return NoContent();
        }
    }
}