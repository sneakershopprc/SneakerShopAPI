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
    public class BrandsController : ControllerBase
    {
        private readonly BrandRepository brandRepository;

        public BrandsController(BrandRepository _brandRepository)
        {
            this.brandRepository = _brandRepository;
        }

        [HttpGet]
        public IActionResult GetAllBrand([FromQuery] SearchBrandVModel model)
        {
            var result = brandRepository.GetAll(model);
            return Ok(result);
        }

        [HttpGet("{brandId}", Name = "GetBrand")]
        public IActionResult GetBrand(string brandId)
        {
            var brand = brandRepository.GetToVModel(brandId);
            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BrandVModel model)
        {
            var result = brandRepository.Create(model);
            if (result != null)
            {
                return Created("", result);
            }

            return BadRequest();
        }

        [HttpPut("{brandId}")]
        public IActionResult Update(string brandId, [FromBody] BrandVModel model)
        {
            var brand = brandRepository.Get(brandId);
            if (brand == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = brandRepository.Update(brandId, model);
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpDelete("{brandId}")]
        public IActionResult Delete(string brandId, [FromQuery] string implementer)
        {
            var result = brandRepository.Delete(brandId, implementer);
            if (result)
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
