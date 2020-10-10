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
    public class OrdersController : ControllerBase
    {
        private readonly OrderRepository orderRepository;

        public OrdersController(OrderRepository _orderRepository)
        {
            this.orderRepository = _orderRepository;
        }

        [HttpGet]
        public IActionResult GetAllOrder([FromQuery] ResourceParameters model)
        {
            var result = orderRepository.GetAll(model);
            return Ok(result);
        }

        [HttpGet("{orderId}", Name = "GetOrder")]
        public IActionResult GetOrder(string orderId)
        {
            var order = orderRepository.GetToVModel(orderId);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderVModel model)
        {
            var result = orderRepository.Create(model);
            if (result != null)
            {
                return Created("", result);
            }

            return BadRequest("There is an item in your cart that is out of stock");
        }

        [HttpPut("{orderId}")]
        public IActionResult Update(string orderId, [FromBody] OrderVModel orderVModel)
        {
            var order = orderRepository.Get(orderId);
            if (order == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = orderRepository.Update(orderId, orderVModel);
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
