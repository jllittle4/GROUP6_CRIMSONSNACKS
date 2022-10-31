using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using api.Models;
using api.Utilities;
using api.Interfaces;
using api.CRUD;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportingTime : ControllerBase
    {
        // GET: api/ReportingTime
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<TimeEvent> Get()
        {
            return new List<TimeEvent>();
        }

        // GET: api/ReportingTime/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetReportingTime")]
        public TimeEvent Get(int id)
        {
            return new TimeEvent();
        }

        // POST: api/ReportingTime
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public List<TimeEvent> Post([FromBody] ReportRequest myReport)
        {
            if (myReport.Type == "time-events-by-emp")
            {
                System.Console.WriteLine("\nReceived request to get report of timekeeping events...");
                System.Console.WriteLine(myReport.ToString());

                IReportTimeEvents myFinder = new ReportTimeEventsByEmp();
                return myFinder.Find(myReport);
            }

            return new List<TimeEvent>();
        }

        // PUT: api/ReportingTime/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ReportRequest myReport)
        {
        }

        // DELETE: api/ReportingTime/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
