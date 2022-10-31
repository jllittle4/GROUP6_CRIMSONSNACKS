using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using api.Interfaces;
using api.Models;
using api.CRUD;
using api.Utilities;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingIn : ControllerBase
    {
        public static User loggedIn = new User();

        // GET: api/LoggingIn
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<LoginResult> Get()
        {
            return new List<LoginResult>();
        }

        // GET: api/LoggingIn/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetLoggingIn")]
        public LoginResult Get(int id)
        {
            return new LoginResult();
        }

        // POST: api/LoggingIn
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public LoginResult Post([FromBody] User newUser)
        {
            LoginResult myLoginAttempt = new LoginResult();

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

            return myLoginAttempt;
        }

        // PUT: api/LoggingIn/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/LoggingIn/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
