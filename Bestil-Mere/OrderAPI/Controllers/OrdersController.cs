using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Order;
using OrderAPI.Models;
using OrderAPI.Services;
using OrderAPI.Utils.Converters;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get() =>
            await _orderService.Get();

        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public async Task<IActionResult> Get(string id)
        {
            var order = _orderService.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok((await order).ToOrderDTO());
        }

        [Route("customerId/{customerId}")]
        [HttpGet]
        public async Task<IActionResult> GetByCustId(string customerId)
        {
            var order = _orderService.GetByCustId(customerId);

            if (order == null)
            {
                return NotFound();
            }
            return Ok((await order).Select(o => o.ToOrderDTO()).ToList());
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody]CreateOrderModel orderModel)
        {
            if (!ModelState.IsValid || orderModel == null)
            {
                return BadRequest(ModelState);
            }
            var order = _orderService.Create(orderModel);
            return Ok((await order).ToOrderDTO());
            //return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
        }


        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var order = await _orderService.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderService.Remove(order.Id);

            return NoContent();
        }
    }
}