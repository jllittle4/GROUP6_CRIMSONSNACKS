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
using api.Utilities;
using api.Adapters;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Requests : ControllerBase
    {
        // GET: api/Requests
        //returns list of all requests for admin view requests page
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<Request> Get()
        {
            System.Console.WriteLine("\nReceived request to get all time change requests...");
            IReadAllRequests reader = new ReadRequests();
            return reader.ReadAllRequests();
        }

        // GET: api/Requests/5
        //returns one request 
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetRequests")]
        public Request Get(int id)
        {
            System.Console.WriteLine("\nReceived request to find time change request...");
            IReadOneRequest reader = new ReadRequests();
            return reader.ReadOneRequest(id);
        }

        // POST: api/Requests
        //creates one request for logged in user
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] Request newRequest)
        {
            System.Console.WriteLine("\nReceived request to create new time change request...");
            ICreateOneRequest creator = new CreateRequest();
            creator.CreateOneRequest(newRequest);
        }

        // PUT: api/Requests/5
        //update request and create or update time event if event is approved by admin, update request if request is denied by admin
        //return request object for status on updating request
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public Request Put(int id, [FromBody] Request updatedRequest)
        {
            //front end sends an id of 0 to handle updating or creating a time event if the user approves a request
            if (id == 0)
            {
                //front end sets request status to "Attempt-To-Be-Approved" to make sure new time event won't conflict with old time events
                //if the request won't conflict with old time events, this loop will create new time event or update old time event
                //if the request will overwrite old time events, a "warning" status is returned to the front end to prompt the user to confirm or deny the overwrite first
                if (updatedRequest.Status == "Attempt-To-Be-Approved")
                {
                    System.Console.WriteLine("\nReceived request to try creating time event from request...");
                    IHandleTimeEventFromReq myHandler = new TimeEventFromReq();
                    return myHandler.FindRequest(updatedRequest);
                }
                //if user approves overwriting old time events, the request is sent back, and will fall through to this loop
                //this loop will delete conflicting time events and create correct event
                //returns status to alert front end 
                else
                {
                    System.Console.WriteLine("\nReceived request to create time event from request..");
                    RequestToTimeEventAdapter myAdapter = new RequestToTimeEventAdapter(updatedRequest);

                    IHandleTimeEventFromReq myHandler = new ApproveConflictRequest();
                    return myHandler.FindRequest(updatedRequest);
                }
            }
            //finally, front end sends a id > 0 to update the status of the request to "approved" or "denied" in the database
            else
            {
                System.Console.WriteLine("\nReceived request to update time change request...");
                IUpdateOneRequest updater = new UpdateRequest();
                updater.UpdateOneRequest(id, updatedRequest);

                return new Request() { Status = "updated" };
            }
        }

        // DELETE: api/Requests/5
        //delete a request...currently not used
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
