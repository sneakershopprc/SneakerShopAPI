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
    public class WishListsController : ControllerBase
    {
        private readonly WishListRepository wishListRepository;

        public WishListsController(WishListRepository _wishListRepository)
        {
            this.wishListRepository = _wishListRepository;
        }

        [HttpGet]
        public IActionResult GetAllWishList([FromQuery] SearchWishListVModel model)
        {
            var result = wishListRepository.GetAll(model);
            return Ok(result);
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


        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id, [FromQuery] string username)
        {
            var wishList = wishListRepository.Get(Id);
            if (wishList == null)
            {
                return NotFound();
            }
            var result = wishListRepository.Delete(Id, username);
            if (result)
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
