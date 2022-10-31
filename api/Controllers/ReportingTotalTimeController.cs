using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Interfaces;
using api.Utilities;
using Microsoft.AspNetCore.Cors;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportingTotalTime : ControllerBase
    {
        // GET: api/ReportingTotalTime
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<Report> Get()
        {
            return new List<Report>();
        }

        // GET: api/ReportingTotalTime/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetReportingTotalTime")]
        public Report Get(int id)
        {
            return new Report();
        }

        // POST: api/ReportingTotalTime
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public List<Report> Post([FromBody] ReportRequest myReport)
        {
            if(myReport.Type == "department-total-time")
            {
                IReportTotalTime myReporter = new ReportTotalTimeByDep();
                return myReporter.Find(int.Parse(myReport.PayrollPeriod));
            }
            else if(myReport.Type == "employee-total-time")
            {
                IReportTotalTime myReporter = new ReportTotalTimeByEmp();
                return myReporter.Find(int.Parse(myReport.PayrollPeriod));
            }

            return new List<Report>();
        }

        // PUT: api/ReportingTotalTime/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ReportRequest myReport)
        {
            
        }

        // DELETE: api/ReportingTotalTime/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
