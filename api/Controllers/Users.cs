using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.AspNetCore.Cors;
using api.database;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        // GET: api/Users
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<User> Get()
        {
            List<User> users = new List<User>();
            


            return users;
        }

        // GET: api/Users/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetUser")]
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
            System.Console.WriteLine(value.UserName);
            
            System.Console.WriteLine("I made it to post");
            System.Console.WriteLine(value.UserName);
            
            SaveUser saveUser = new SaveUser();
            saveUser.temp = new User();
           
            saveUser.temp.UserName = value.UserName;
            saveUser.temp.FirstName = value.FirstName;
            saveUser.temp.LastName = value.LastName;

            saveUser.temp.Password = value.Password;

            // System.Console.WriteLine(saveUser.temp.EmpName);
            saveUser.SeedUser();
            
            
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
