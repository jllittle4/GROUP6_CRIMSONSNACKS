using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.AspNetCore.Cors;
using api.Interfaces;
using api.CRUD;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeKeeping : ControllerBase
    {
        // GET: api/TimeKeeping
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<TimeEvent> Get()
        {
            //List<TimeEvent> users = new List<TimeEvent>();
            //users.Add(new TimeEvent()); //{FirstName = "Jeremy", LastName = "Little", UserName = "jllittle", Password = "Jman040402$"});
            System.Console.WriteLine("\nReceived request to get all timekeeping events...");

            IReadAllTimeEvents readerAll = new ReadTimeEvents();

            return readerAll.ReadAllTimeEvents();
        }

        // GET: api/TimeKeeping/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetTimeKeeping")]
        public TimeEvent Get(int id)
        {
            System.Console.WriteLine("\nReceived request to find timekeeping event...");
            
            IReadOneTimeEvent readerOne = new ReadTimeEvents();
            
            return readerOne.ReadOneTimeEvent(id);
        }

        // POST: api/TimeKeeping
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] TimeEvent newTimeEvent)
        {
            System.Console.WriteLine("\nReceived request to create new timekeeping event...");
            //System.Console.WriteLine(newDepartment.ToString());

            ICreateOneTimeEvent creator = new CreateTimeEvent();
            creator.CreateOneTimeEvent(newTimeEvent);
        }

        // PUT: api/TimeKeeping/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TimeEvent updatedTimeEvent)
        {
            System.Console.WriteLine("\nReceived request to update timekeeping event...");
            //System.Console.WriteLine(updatedDepartment.ToString());

            IUpdateOneTimeEvent updater = new UpdateTimeEvent();
            updater.UpdateOneTimeEvent(id, updatedTimeEvent);
        }

        // DELETE: api/TimeKeeping/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            System.Console.WriteLine("\nReceived request to delete timekeeping event...");

            IDeleteOne deleteTool = new DeleteTimeEvent();
            deleteTool.DeleteOne(id);
        }
    }
}
