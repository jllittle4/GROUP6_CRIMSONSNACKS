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
using api.Utilities;

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
            System.Console.WriteLine("\nReceived request to get all timekeeping events...");

            IReadAllTimeEvents myFinder = new FindTimeEventsByEmp();
            
            return myFinder.ReadAllTimeEvents();
        }

        // GET: api/TimeKeeping/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetTimeKeeping")]
        public TimeEvent Get(int id)
        {
            TimeEvent myTimeEvent = new TimeEvent();

            if (id == 0)
            {
                System.Console.WriteLine("\nRequest to find timekeeping event without id...");

                IFindRecentTimeEvent myFinder = new FindMostRecentTimeEvent();
                myTimeEvent = myFinder.FindTimeEvent();
            }
            else
            {
                System.Console.WriteLine("\nReceived request to find timekeeping event...");

                IReadOneTimeEvent readerOne = new ReadTimeEvents();
                myTimeEvent = readerOne.ReadOneTimeEvent(id);
            }

            return myTimeEvent;
        }

        // POST: api/TimeKeeping
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] TimeEvent newTimeEvent)
        {
            System.Console.WriteLine("\nReceived request to clock in...");

            CreateTimeEvent creator = new CreateTimeEvent();
            creator.ClockingIn();
        }

        // PUT: api/TimeKeeping/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TimeEvent updatedTimeEvent)
        {
            if (id == 0)
            {
                System.Console.WriteLine("\nReceived request to clock out...");
                UpdateTimeEvent updater = new UpdateTimeEvent();
                updater.ClockingOut(updatedTimeEvent);
            }
            else 
            {
                System.Console.WriteLine("\nReceived request to update timekeeping event...");
                IUpdateOneTimeEvent updater = new UpdateTimeEvent();
                updater.UpdateOneTimeEvent(id, updatedTimeEvent);
            }

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
