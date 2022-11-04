using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using api.Models;
using api.Interfaces;
using api.Utilities;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportingTotalTime : ControllerBase
    {
        // GET: api/ReportingTotalTime
        //returns empty list of report objects...currently not used
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<Report> Get()
        {
            return new List<Report>();
        }

        // GET: api/ReportingTotalTime/5
        //returns empty report object...currently not used
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetReportingTotalTime")]
        public Report Get(int id)
        {
            return new Report();
        }

        // POST: api/ReportingTotalTime
        //returns list of report objects based on requested report criteria in reportrequest object
        //designed with capability for more types of total reports
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public List<Report> Post([FromBody] ReportRequest myReport)
        {
            if(myReport.Type == "department-total-time")
            {
                IReportTotalTime myReporter = new ReportTotalTimeByDep();
                return myReporter.Find((myReport.PayrollPeriod));
            }
            else if(myReport.Type == "employee-total-time")
            {
                IReportTotalTime myReporter = new ReportTotalTimeByEmp();
                return myReporter.Find((myReport.PayrollPeriod));
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
