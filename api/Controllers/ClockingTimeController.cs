using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using api.Models;
using api.CRUD;
using api.Interfaces;
using api.Utilities;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClockingTime : ControllerBase
    {
        // GET: api/ClockingTime
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<TimeEvent> Get()
        {
            System.Console.WriteLine("\nReceived request to get all timekeeping events...");
            IReadAllTimeEvents myFinder = new FindTimeEventsByEmp();
            return myFinder.ReadAllTimeEvents();
        }

        // GET: api/ClockingTime/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetClockingTime")]
        public TimeEvent Get(int id)
        {
            System.Console.WriteLine("\nRequest to find timekeeping event without id...");
            IFindRecentTimeEvent myFinder = new FindMostRecentTimeEvent();
            return myFinder.FindTimeEvent();
        }

        // POST: api/ClockingTime
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] TimeEvent myTimeEvent)
        {
            System.Console.WriteLine("\nReceived request to clock in...");
            CreateTimeEvent creator = new CreateTimeEvent();
            creator.ClockingIn();
        }

        // PUT: api/ClockingTime/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TimeEvent updatedTimeEvent)
        {
            System.Console.WriteLine("\nReceived request to clock out...");
            UpdateTimeEvent updater = new UpdateTimeEvent();
            updater.ClockingOut(updatedTimeEvent);
        }

        // DELETE: api/ClockingTime/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
