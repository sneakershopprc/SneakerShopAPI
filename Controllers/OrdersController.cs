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
using ssrcore.Helpers;
using ssrcore.ViewModels;
using static ssrcore.Helpers.Constants;

namespace SneakerShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly OrderRepository orderRepository;

        public OrdersController(OrderRepository _orderRepository)
        {
            this.orderRepository = _orderRepository;
        }

        [HttpGet]
        public IActionResult GetAllOrder([FromQuery] SearchOrderVModel model)
        {
            if (GetCurrentRole().Equals(ParticipantsRoleConst.CUSTOMER))
            {
                model.Username = GetCurrentUser();
            }
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
        [Authorize(Roles = ParticipantsRoleConst.CUSTOMER)]
        public IActionResult Create([FromBody] OrderVModel model)
        {
            model.Username = GetCurrentUser();
            var result = orderRepository.Create(model);
            if (result != null)
            {
                return Created("", result);
            }

            return BadRequest("There is an item in your cart that is out of stock");
        }

        [HttpPut("{orderId}")]
        [Authorize(Roles = ParticipantsRoleConst.ADMIN)]
        public IActionResult Update(string orderId, [FromBody] OrderVModel orderVModel)
        {
            var order = orderRepository.Get(orderId);
            if (order == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                orderVModel.Username = GetCurrentUser();
                var result = orderRepository.Update(orderId, orderVModel);
                return Ok(result);
            }

            return BadRequest();
        }
        private string GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string participantIdVal = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

            return participantIdVal;
        }

        private string GetCurrentRole()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string role = identity.FindFirst(ClaimTypes.Role).Value;

            return role;
        }
    }
}
