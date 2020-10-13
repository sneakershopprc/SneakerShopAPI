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

namespace SneakerShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountRepository repository;

        public AccountsController(AccountRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("profile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult GetProfile()
        {
            var username = this.GetCurrentUser();

            return Ok(repository.GetById(username));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult GetList()
        {
            if (this.GetCurrentRole().Equals("admin"))
            {
                return Ok(repository.GetList(null));
            }
            return Ok(repository.GetList("customer"));
        }

        [HttpDelete("{username}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.ParticipantsRoleConst.ADMIN)]
        public ActionResult SwitchModeAcc(string username)
        {
            if (this.GetCurrentUser().Equals(username)) return BadRequest("Cannot delete yourself");

            if (repository.SwitchAccountMode(username))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{username}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.ParticipantsRoleConst.ADMIN)]
        public ActionResult GetById(string username)
        {
            var acc = repository.GetById(username);

            if (acc != null) return Ok(acc);
            return NotFound();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult UpdateProfile([FromBody] AccountVModel account)
        {
            var username = this.GetCurrentUser();
            account.Username = username;
            var result = repository.UpdateAccount(account);

            if (result != null) return Ok(result);
            return BadRequest("Cannot update profile");
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