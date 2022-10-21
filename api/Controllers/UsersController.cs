using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.AspNetCore.Cors;
using api.database;
using api.Utilities;
using api.Interfaces;
using api.CRUD;

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
        [HttpGet("{id}", Name = "GetUsers")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] User value)
        {
            if (value.FirstName != null)
            {
                System.Console.WriteLine("I made it to post");
                System.Console.WriteLine(value.UserName);

                System.Console.WriteLine("I made it to post");
                System.Console.WriteLine(value.UserName);

                ICreateOneUser saveUser = new CreateUser();
                

                // System.Console.WriteLine(saveUser.temp.EmpName);
                saveUser.CreateOneUser(value);
            }
            if (value.FirstName == null)
            {
                LogInCheck login = new LogInCheck();
                login.temp.UserName = value.UserName;
                login.temp.Password = value.Password;
                login.FindUser();

            }

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
