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
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationRepository repository;

        public AuthenticationController(AuthenticationRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Authentication
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { repository.Get() };
        }

        // GET: api/Authentication/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Authentication
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Authentication/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
