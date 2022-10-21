using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.AspNetCore.Cors;
using api.database;
using api.Utilities;
using api.Interfaces;
using api.CRUD;

namespace api.Controllers
{
    //not done
    [Route("api/[controller]")]
    [ApiController]
    public class Requests : ControllerBase
    {
        // GET: api/Requests
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<Request> Get()
        {
            List<Request> requests = new List<Request>();



            return requests;
        }

        // GET: api/Requests/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetRequest")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Requests
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] Request value)
        {
            if (value.Reason != null)
            {
                System.Console.WriteLine("I made it to post");
                //System.Console.WriteLine(value.UserName);

                System.Console.WriteLine("I made it to post");
                //System.Console.WriteLine(value.UserName);

                ICreateOneRequest myRequest = new CreateRequest();
                

                // System.Console.WriteLine(saveUser.temp.EmpName);
                myRequest.CreateOneRequest(value);
            }
            

        }

        // PUT: api/Requests/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Requests/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
