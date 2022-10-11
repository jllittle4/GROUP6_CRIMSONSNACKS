using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.AspNetCore.Cors;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/Users
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<User> Get()
        {
            List<User> users = new List<User>();
            users.Add(new User(){FirstName = "Jeremy", LastName = "Little", UserId = "jllittle", Password = "Jman040402$"});


            return users;
        }

        // GET: api/Users/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] User value)
        {
            System.Console.WriteLine("I made it to post");
            System.Console.WriteLine(value.UserId);
            
        }

        // PUT: api/Users/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Users/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
