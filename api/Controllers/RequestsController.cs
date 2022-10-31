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
    public class Requests : ControllerBase
    {
        // GET: api/Requests
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<Request> Get()
        {
            System.Console.WriteLine("\nReceived request to get all time change requests...");
            IReadAllRequests readerAll = new ReadRequests();
            return readerAll.ReadAllRequests();
        }

        // GET: api/Requests/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetRequests")]
        public Request Get(int id)
        {
            System.Console.WriteLine("\nReceived request to find time change request...");
            IReadOneRequest readerOne = new ReadRequests();
            return readerOne.ReadOneRequest(id);
        }

        // POST: api/Requests
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] Request newRequest)
        {          
            System.Console.WriteLine("\nReceived request to create new time change request...");
            //System.Console.WriteLine(newDepartment.ToString());

            ICreateOneRequest creator = new CreateRequest();
            creator.CreateOneRequest(newRequest);
        }

        // PUT: api/Requests/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Request updatedRequest)
        {
            System.Console.WriteLine("\nReceived request to update time change request...");
            //System.Console.WriteLine(updatedDepartment.ToString());

            IUpdateOneRequest updater = new UpdateRequest();
            updater.UpdateOneRequest(id, updatedRequest);
        }

        // DELETE: api/Requests/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            System.Console.WriteLine("\nReceived request to delete time change request...");

            IDeleteOne deleteTool = new DeleteRequest();
            deleteTool.DeleteOne(id);
        }
    }
}
