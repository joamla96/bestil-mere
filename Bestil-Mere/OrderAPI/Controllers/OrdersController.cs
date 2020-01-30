using System;
using System.Collections.Generic;
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
        public ActionResult<List<Order>> Get() =>
            _orderService.Get();

        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public IActionResult Get(string id)
        {
            var order = _orderService.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(OrderConverter.ToOrderDTO(order));
        }

        [HttpPost]
        public ActionResult<Order> Create([FromBody]CreateOrderModel orderModel)
        {
            var order = _orderService.Create(orderModel);
            return Ok(OrderConverter.ToOrderDTO(order));
            //return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
        }


        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var order = _orderService.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderService.Remove(order.Id);

            return NoContent();
        }
    }
}