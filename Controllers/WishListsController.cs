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
using ssrcore.ViewModels;
using static ssrcore.Helpers.Constants;

namespace SneakerShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WishListsController : ControllerBase
    {
        private readonly WishListRepository wishListRepository;

        public WishListsController(WishListRepository _wishListRepository)
        {
            this.wishListRepository = _wishListRepository;
        }

        [HttpGet]
        [Authorize(Roles = ParticipantsRoleConst.CUSTOMER)]
        public IActionResult GetAllWishList([FromQuery] ResourceParameters model)
        {
            string username = GetCurrentUser();
            var result = wishListRepository.GetAll(model, username);
            return Ok(result);
        }



        [HttpGet("{productId}")]
        [Authorize(Roles = ParticipantsRoleConst.CUSTOMER)]
        public IActionResult Get(string productId)
        {
            string username = GetCurrentUser();
            var wishlist = wishListRepository.GetToVModel(productId, username);
            if (wishlist == null)
            {
                return Ok();
            }

            return Ok(wishlist);
        }
        [HttpPost]
        [Authorize(Roles = ParticipantsRoleConst.CUSTOMER)]
        public IActionResult Create([FromBody] WishListVModel model)
        {
            model.Username = GetCurrentUser();
            var result = wishListRepository.Create(model);
            if (result)
            {
                return Created("", result);
            }

            return BadRequest();
        }


        [HttpDelete("{productId}")]
        [Authorize(Roles = ParticipantsRoleConst.CUSTOMER)]
        public IActionResult Delete(string productId)
        {
            string username = GetCurrentUser();
            var wishList = wishListRepository.Get(productId, username);
            if (wishList == null)
            {
                return NotFound();
            }
            var result = wishListRepository.Delete(productId, username);
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
