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
    public class PhotoProductController : ControllerBase
    {
        private readonly PhotoProductRepository photoProductRepository;

        public PhotoProductController(PhotoProductRepository _photoProductRepository)
        {
            this.photoProductRepository = _photoProductRepository;
        }

        //[HttpPost]
        //public IActionResult Create([FromBody] PhotoProduct model)
        //{
        //    var result = photoProductRepository.Create(model);
        //    if (result != null)
        //    {
        //        return Created("", result);
        //    }

        //    return BadRequest();
        //}

        //[HttpDelete("{photo}")]
        //public IActionResult Delete(string photo)
        //{
        //    var result = photoProductRepository.Delete(photo);
        //    if (result)
        //    {
        //        return NoContent();
        //    }

        //    return BadRequest();
        //}
    }
}
