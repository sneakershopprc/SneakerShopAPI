using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SneakerShopAPI.Repositories;
using SneakerShopAPI.ViewModels;
using static ssrcore.Helpers.Constants;

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantsRoleConst.ADMIN)]
        public IActionResult Create([FromBody] ProductVModel model)
        {
            model.Implementer = GetCurrentUser();
            var result = productRepository.Create(model);
            if (result != null)
            {
                return Created("", result);
            }

            return BadRequest();
        }

        [HttpPut("{productId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantsRoleConst.ADMIN)]
        public IActionResult Update(string productId, [FromBody] ProductVModel model)
        {
            var product = productRepository.Get(productId);
            if (product == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                model.Implementer = GetCurrentUser();
                var result = productRepository.Update(productId, model);
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpDelete("{productId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantsRoleConst.ADMIN)]
        public IActionResult Delete(string productId)
        {
            var product = productRepository.Get(productId);
            if (product == null)
            {
                return NotFound();
            }
            string implementer = GetCurrentUser();
            var result = productRepository.Delete(productId, implementer);
            if (result)
            {
                return NoContent();
            }

            return BadRequest();
        }
        private string GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string participantIdVal = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

            return participantIdVal;
        }
    }
}
