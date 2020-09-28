using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SneakerShopAPI.Models;
using SneakerShopAPI.Repositories;

namespace SneakerShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly ProductDetailRepository productDetailRepository;

        public ProductDetailsController(ProductDetailRepository _productDetailRepository)
        {
            this.productDetailRepository = _productDetailRepository;
        }

        [HttpGet]
        public IActionResult GetAllProductDetail([FromQuery] string productId, [FromQuery] int isStill)
        {
            var result = productDetailRepository.GetAll(productId, isStill);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductDetail model)
        {
            var result = productDetailRepository.Create(model);
            if (result != null)
            {
                return Created("", result);
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProductDetail model)
        {
            var productDetail = productDetailRepository.Get(model.ProductId, model.Size);
            if (productDetail == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = productDetailRepository.Update(model);
                return Ok(result);
            }

            return BadRequest();
        }

    }
}
