using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using LogisticsAPI.Models;
using LogisticsAPI.Services;

namespace LogisticsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly ILogisticsPartnerService _service;

        public OrdersController(ILogisticsPartnerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ICollection<Partner>> Get() =>
            await _service.Get();

        [HttpGet("{id:length(24)}", Name = "GetPartner")]
        public async Task<IActionResult> Get(string id)
        {
            var partner = await _service.Get(id);

            if (partner == null)
                return NotFound();

            return Ok(partner);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLogisticsPartnerDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
                return BadRequest();

            var partner = new Partner()
            {
                Name = dto.Name
            };

            await _service.Insert(partner);

            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Partner input)
        {
            var item = await _service.Get(id);
            if (item == null)
                return NotFound();

            await _service.Update(id, input);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _service.Get(id);

            if (item == null)
                return NotFound();

            await _service.Remove(id);

            return NoContent();
        }
    }
}