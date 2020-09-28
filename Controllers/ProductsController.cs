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
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository productRepository;

        public ProductsController(ProductRepository _productRepository)
        {
            this.productRepository = _productRepository;
        }

        [HttpGet]
        public IActionResult GetAllProduct([FromQuery] SearchProductVModel model)
        {
            var result = productRepository.GetAll(model);
            return Ok(result);
        }

        [HttpGet("{productId}", Name = "GetProduct")]
        public IActionResult GetProduct(string productId, [FromQuery] int isStill)
        {
            var product = productRepository.GetToVModel(productId, isStill);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductVModel model)
        {
            var result = productRepository.Create(model);
            if (result != null)
            {
                return Created("", result);
            }

            return BadRequest();
        }

        [HttpPut("{productId}")]
        public IActionResult Update(string productId, [FromBody] ProductVModel model)
        {
            var product = productRepository.Get(productId);
            if (product == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = productRepository.Update(productId, model);
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpDelete("{productId}")]
        public IActionResult Delete(string productId, [FromQuery] string implementer)
        {
            var result = productRepository.Delete(productId, implementer);
            if (result)
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
