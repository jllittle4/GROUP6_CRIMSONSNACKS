using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.AspNetCore.Cors;
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
            System.Console.WriteLine("\nReceived request to get all users...");

            IReadAllUsers readerAll = new ReadUsers();

            return readerAll.ReadAllUsers();
        }

        // GET: api/Users/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetUsers")]
        public User Get(int id)
        {
            System.Console.WriteLine("\nReceived request to find a user...");

            IReadOneUser readerOne = new ReadUsers();

            return readerOne.ReadOneUser(id);
        }

        // POST: api/Users
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] User newUser)
        {
            if (newUser.FirstName != null)
            {
                System.Console.WriteLine("\nReceived request to create new user...");

                ICreateOneUser creator = new CreateUser();

                creator.CreateOneUser(newUser);
            }
            else if (newUser.FirstName == null)
            {
                System.Console.WriteLine("Received login request...");

                LogInCheck login = new LogInCheck();
                login.Check(newUser);
            }
        }

        // PUT: api/Users/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User updatedUser)
        {
            System.Console.WriteLine("\nReceived request to update user...");
            //System.Console.WriteLine(updatedDepartment.ToString());

            IUpdateOneUser updater = new UpdateUser();
            updater.UpdateOneUser(id, updatedUser);
        }

        // DELETE: api/Users/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            System.Console.WriteLine("\nReceived request to delete user...");

            IDeleteOne deleteTool = new DeleteUser();
            deleteTool.DeleteOne(id);
        }
    }
}
