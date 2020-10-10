using System.Security.Claims;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantsRoleConst.ADMIN)]
        public IActionResult Create([FromBody] BrandVModel model)
        {
            model.Implementer = GetCurrentUser();
            var result = brandRepository.Create(model);
            if (result != null)
            {
                return Created("", result);
            }

            return BadRequest();
        }

        [HttpPut("{brandId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantsRoleConst.ADMIN)]
        public IActionResult Update(string brandId, [FromBody] BrandVModel model)
        {
            model.Implementer = GetCurrentUser();
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantsRoleConst.ADMIN)]
        public IActionResult Delete(string brandId)
        {
            string implementer = GetCurrentUser();
            var brand = brandRepository.Get(brandId);
            if (brand == null)
            {
                return NotFound();
            }
            var result = brandRepository.Delete(brandId, implementer);
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
