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
    public class TimeKeeping : ControllerBase
    {
        // GET: api/TimeKeeping
        //return list of all time events...currently not used
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<TimeEvent> Get()
        {
            System.Console.WriteLine("\nReceived request to get all timekeeping events...");
            IReadAllTimeEvents finder = new ReadTimeEvents();
            return finder.ReadAllTimeEvents();
        }

        // GET: api/TimeKeeping/5
        //return one time event
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetTimeKeeping")]
        public TimeEvent Get(int id)
        {
            System.Console.WriteLine("\nReceived request to find timekeeping event...");
            IReadOneTimeEvent reader = new ReadTimeEvents();
            return reader.ReadOneTimeEvent(id);
        }

        // POST: api/TimeKeeping
        //create time event...currently not used by frontend
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] TimeEvent newTimeEvent)
        {
            System.Console.WriteLine("\nReceived request to create timekeeping event...");
            ICreateOneTimeEvent creator = new CreateTimeEvent();
            creator.CreateOneTimeEvent(newTimeEvent);
        }

        // PUT: api/TimeKeeping/5
        //update time event...currnetly not used by frontend
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TimeEvent updatedTimeEvent)
        {
            System.Console.WriteLine("\nReceived request to update timekeeping event...");
            IUpdateOneTimeEvent updater = new UpdateTimeEvent();
            updater.UpdateOneTimeEvent(id, updatedTimeEvent);
        }

        // DELETE: api/TimeKeeping/5
        //delete time event...currently not used
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
