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
        public IActionResult GetAllProductDetail([FromQuery] string productId)
        {
            var result = productDetailRepository.GetAll(productId);
            return Ok(result);
        }

        [HttpGet("{Id}", Name = "GetProductDetail")]
        public IActionResult GetProductDetail(int Id)
        {
            var productDetail = productDetailRepository.GetVModel(Id);
            if (productDetail == null)
            {
                return NotFound();
            }

            return Ok(productDetail);
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

        [HttpPut("{Id}")]
        public IActionResult Update(int Id, [FromBody] ProductDetail model)
        {
            var productDetail = productDetailRepository.Get(Id);
            if (productDetail == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = productDetailRepository.Update(Id, model);
                return Ok(result);
            }

            return BadRequest();
        }

    }
}
