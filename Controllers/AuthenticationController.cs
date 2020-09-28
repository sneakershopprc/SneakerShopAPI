using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using SneakerShopAPI.Models;
using SneakerShopAPI.Repositories;
using SneakerShopAPI.ViewModels;

namespace SneakerShopAPI.Controllers
{
    [Route("api")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationRepository repository;

        public AuthenticationController(AuthenticationRepository repository)
        {
            this.repository = repository;
        }

        //POST: /user
        [HttpPost("user")]
        public ActionResult Register([FromBody] AccountVModel user)
        {
            if ((user?.Username ?? "").Length < 6 || (user?.Password ?? "").Length < 6)
                return BadRequest("Username and Password must be more than 6 characters");

            try
            {
                return Ok(repository.SignUp(user));
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest($"Username {user.Username} is existed!");
                }
                throw ex;
            }
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] AccountVModel user)
        {
            if ((user?.Username ?? "").Length < 6 || (user?.Password ?? "").Length < 6)
            {
                return BadRequest("Username or Password is incorrect");
            }

            AccountVModel vmodel = repository.LogIn(user);

            if (vmodel == null)
            {
                return BadRequest("Username or Password is incorrect");
            }

            return Ok(vmodel);
        }
    }
}
