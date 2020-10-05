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
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailRepository orderDetailRepository;

        public OrderDetailsController(OrderDetailRepository _orderDetailRepository)
        {
            this.orderDetailRepository = _orderDetailRepository;
        }

        [HttpGet]
        public IActionResult GetAllOrderDetail([FromQuery] SearchOrderVModel model)
        {
            var result = orderDetailRepository.GetAll(model);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CheckOrderDetail([FromBody] List<OrderDetailVModel> model)
        {
            var result = orderDetailRepository.checkOrderDetail(model);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
