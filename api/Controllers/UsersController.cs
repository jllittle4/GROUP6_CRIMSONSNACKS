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
        public static User loggedIn = new User();
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
        public LoginResult Post([FromBody] User newUser)
        {
            LoginResult myLoginAttempt = new LoginResult();

            if (newUser.FirstName != "default")
            {
                System.Console.WriteLine("\nReceived request to create new user...");

                ICreateOneUser creator = new CreateUser();

                creator.CreateOneUser(newUser);
            }
            else
            {
                System.Console.WriteLine("\nReceived login request...");

                IFindUserByUsername myFinder = new FindUserByUsername();
                User tempUser = myFinder.Find(newUser.UserName);

                try
                {
                    LogInCheck login = new LogInCheck(tempUser);
                    myLoginAttempt = login.CheckValidPassword(newUser.Password);
                    System.Console.WriteLine("\n" + myLoginAttempt.ToString());

                    if (myLoginAttempt.CheckUserName && myLoginAttempt.CheckPassword)
                    {
                        loggedIn = tempUser;
                    }
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Invalid username. \n");
                    System.Console.WriteLine(myLoginAttempt.ToString());
                    System.Console.WriteLine(e.ToString());
                }
            }

            return myLoginAttempt;
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
