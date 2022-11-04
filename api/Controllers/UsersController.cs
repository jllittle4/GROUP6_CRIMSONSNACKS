using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using api.Models;
using api.Interfaces;
using api.CRUD;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        // GET: api/Users
        //returns list of all users to populate dropdowns
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<User> Get()
        {
            System.Console.WriteLine("\nReceived request to get all users...");
            IReadAllUsers reader = new ReadUsers();
            return reader.ReadAllUsers();
        }

        // GET: api/Users/5
        //returns one user
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetUsers")]
        public User Get(int id)
        {
            System.Console.WriteLine("\nReceived request to find a user...");
            IReadOneUser reader = new ReadUsers();
            return reader.ReadOneUser(id);
        }

        // POST: api/Users
        //creates new user for registration
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public LoginResult Post([FromBody] User newUser)
        {
            LoginResult myLoginAttempt = new LoginResult();
            System.Console.WriteLine("\nReceived request to create new user...");
            ICreateOneUser creator = new CreateUser();
            creator.CreateOneUser(newUser);
            return myLoginAttempt;
        }

        // PUT: api/Users/5
        //updates user...currently not used
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User updatedUser)
        {
            System.Console.WriteLine("\nReceived request to update user...");
            IUpdateOneUser updater = new UpdateUser();
            updater.UpdateOneUser(id, updatedUser);
        }

        // DELETE: api/Users/5
        //deletes user...currently not used
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
