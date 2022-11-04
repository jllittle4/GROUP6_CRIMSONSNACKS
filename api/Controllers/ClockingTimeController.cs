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
        //returns list of all time events based on the logged in user
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<TimeEvent> Get()
        {
            System.Console.WriteLine("\nReceived request to get all timekeeping events...");
            IReadAllTimeEvents finder = new FindTimeEventsByEmp();
            return finder.ReadAllTimeEvents();
        }

        // GET: api/ClockingTime/5
        //returns most recent time event for clockedoutcheck
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetClockingTime")]
        public TimeEvent Get(int id)
        {
            System.Console.WriteLine("\nRequest to find timekeeping event without id...");
            IFindRecentTimeEvent finder = new FindMostRecentTimeEvent();
            return finder.FindTimeEvent();
        }

        // POST: api/ClockingTime
        //creates time event for logged in user, marked as a clockin
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] TimeEvent myTimeEvent)
        {
            System.Console.WriteLine("\nReceived request to clock in...");
            IClockIn clocker = new ClockIn();
            clocker.ClockingIn();
        }

        // PUT: api/ClockingTime/5
        //updates a time event for logged in user, marked as a clockout
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TimeEvent updatedTimeEvent)
        {
            System.Console.WriteLine("\nReceived request to clock out...");
            IClockOut clocker = new ClockOut();
            clocker.ClockingOut(updatedTimeEvent);
        }

        // DELETE: api/ClockingTime/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
