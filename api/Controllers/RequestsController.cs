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
using api.Adapters;

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
        public Request Put(int id, [FromBody] Request updatedRequest)
        {
            if (id == 0)
            {
                if (updatedRequest.Status == "Attempt-To-Be-Approved")
                {
                    //this will create a new time event or update a time event if there are no conflicts
                    System.Console.WriteLine("\nReceived request to try creating time event from request...");
                    IHandleTimeEventFromReq myHandler = new TimeEventFromReq();
                    return myHandler.FindRequest(updatedRequest);
                }
                else
                {
                    System.Console.WriteLine("\nReceived request to create time event from request..");
                    RequestToTimeEventAdapter myAdapter = new RequestToTimeEventAdapter(updatedRequest);

                    //this needs to delete conflicting time events
                    IHandleTimeEventFromReq myHandler = new ApproveConflictRequest();
                    return myHandler.FindRequest(updatedRequest);
                }

            }
            else
            {
                System.Console.WriteLine("\nReceived request to update time change request...");

                IUpdateOneRequest updater = new UpdateRequest();
                updater.UpdateOneRequest(id, updatedRequest);

                return new Request() { Status = "updated" };
            }

            //return new Request();
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
