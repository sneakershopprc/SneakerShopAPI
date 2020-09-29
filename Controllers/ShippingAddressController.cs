using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SneakerShopAPI.Repositories;
using SneakerShopAPI.ViewModels;

namespace SneakerShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingAddressController : ControllerBase
    {
        private readonly ShippingAddressRepository shippingAddressRepository;

        public ShippingAddressController(ShippingAddressRepository _shippingAddressRepository)
        {
            this.shippingAddressRepository = _shippingAddressRepository;
        }

        [HttpGet]
        public IActionResult GetAllShippingAddress([FromQuery] SearchShippingAddressVModel model)
        {
            var result = shippingAddressRepository.GetAll(model);
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] ShippingAddressVModel model)
        {
            var result = shippingAddressRepository.Create(model);
            if (result)
            {
                return Created("", result);
            }

            return BadRequest();
        }

        [HttpPut("{Id}")]
        public IActionResult Update(int Id, [FromBody] ShippingAddressVModel model)
        {
            var shippingAddress = shippingAddressRepository.Get(Id);
            if (shippingAddress == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = shippingAddressRepository.Update(Id, model);
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id, [FromQuery] string username)
        {
            var shippingAddress = shippingAddressRepository.Get(Id);
            if (shippingAddress == null)
            {
                return NotFound();
            }
            var result = shippingAddressRepository.Delete(Id, username);
            if (result)
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
