using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.AspNetCore.Cors;
using api.Interfaces;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeKeeping : ControllerBase
    {
        // GET: api/TimeKeeping
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<TimeEvent> GetTime()
        {
            List<TimeEvent> users = new List<TimeEvent>();
            users.Add(new TimeEvent()); //{FirstName = "Jeremy", LastName = "Little", UserName = "jllittle", Password = "Jman040402$"});


            return users;
        }

        // GET: api/TimeKeeping/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetTimeKeeping")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TimeKeeping
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] TimeEvent value)
        {
            System.Console.WriteLine("I made it to post");
            System.Console.WriteLine();//value.UserName);
            
        }

        // PUT: api/TimeKeeping/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: api/TimeKeeping/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
