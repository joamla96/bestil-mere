using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Restaurant;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<RestaurantDTO>>> Get() =>
            await _restaurantService.Get();

        [HttpGet("{id:length(24)}", Name = "GetRestaurant")]
        public async Task<ActionResult<RestaurantDTO>> Get(string id)
        {
            var restaurant = _restaurantService.Get(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(await restaurant);
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantDTO>> Create([FromBody]CreateRestaurantModel crm)
        {
            if (!ModelState.IsValid || crm == null)
                return BadRequest(ModelState);
            
            var restaurant = _restaurantService.Create(crm);
            return Ok(await restaurant);
            //return CreatedAtRoute("GetRestaurant", new { id = restaurant.Id.ToString() }, restaurant);
        }

        [HttpPut]
        public IActionResult Update([FromBody]UpdateRestaurantModel restaurantIn)
        {
            var restaurant = _restaurantService.Get(restaurantIn.Id);

            if (restaurant == null)
            {
                return NotFound();
            }

            _restaurantService.Update(restaurantIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var restaurant = await _restaurantService.Get(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            _restaurantService.Remove(restaurant.Id.ToString());

            return NoContent();
        }
    }
}