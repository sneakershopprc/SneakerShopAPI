using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SneakerShopAPI.Repositories;
using SneakerShopAPI.ViewModels;
using ssrcore.ViewModels;

namespace SneakerShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListsController : ControllerBase
    {
        private readonly WishListRepository wishListRepository;

        public WishListsController(WishListRepository _wishListRepository)
        {
            this.wishListRepository = _wishListRepository;
        }

        [HttpGet]
        public IActionResult GetAllWishList([FromQuery] ResourceParameters model)
        {
            var result = wishListRepository.GetAll(model);
            return Ok(result);
        }



        [HttpGet("{productId}")]
        public IActionResult Get(string productId)
        {
            var wishlist = wishListRepository.GetToVModel(productId);
            if (wishlist == null)
            {
                return Ok();
            }

            return Ok(wishlist);
        }
        [HttpPost]
        public IActionResult Create([FromBody] WishListVModel model)
        {
            var result = wishListRepository.Create(model);
            if (result)
            {
                return Created("", result);
            }

            return BadRequest();
        }


        [HttpDelete("{productId}")]
        public IActionResult Delete(string productId)
        {
            var wishList = wishListRepository.Get(productId);
            if (wishList == null)
            {
                return NotFound();
            }
            var result = wishListRepository.Delete(productId);
            if (result)
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
