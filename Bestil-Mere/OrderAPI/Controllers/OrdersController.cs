using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<List<Order>>> Get() =>
            await _orderService.Get();

        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public async Task<IActionResult> Get(string id)
        {
            var order = _orderService.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(OrderConverter.ToOrderDTO(await order));
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody]CreateOrderModel orderModel)
        {
            var order = _orderService.Create(orderModel);
            return Ok(OrderConverter.ToOrderDTO(await order));
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