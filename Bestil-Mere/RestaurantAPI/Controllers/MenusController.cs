using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Restaurant;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : Controller
    {
        private readonly IMenuService _menuService;

        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        
        [HttpGet("{id:length(24)}", Name = "GetMenu")]
        public async Task<ActionResult<MenuDTO>> Get(string id)
        {
            var menu = await _menuService.Get(id);

            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu);
        }

        [HttpPut]
        public IActionResult Update([FromBody]UpdateMenuModel menuIn)
        {
            if (!ModelState.IsValid || menuIn == null)
                return BadRequest(ModelState);
            
            var menu = _menuService.Get(menuIn.Id);

            if (menu == null)
            {
                return NotFound();
            }

            _menuService.Update(menuIn);

            return NoContent();
        }

    }
}