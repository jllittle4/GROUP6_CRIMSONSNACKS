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

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Departments : ControllerBase
    {
        // GET: api/Departments
        //return list of all departments to populate dropdowns
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<Department> Get()
        {
            System.Console.WriteLine("\nReceived request to get all departments...");
            IReadAllDepartments reader = new ReadDepartments();
            return reader.ReadAllDepartments();
        }

        // GET: api/Departments/5
        //returns one department...currently not used
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetDepartments")]
        public Department Get(int id)
        {
            System.Console.WriteLine("\nReceived request to find department...");
            IReadOneDepartment reader = new ReadDepartments();
            return reader.ReadOneDepartment(id);
        }

        // POST: api/Departments
        //creates new department...currently not used
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] Department newDepartment)
        {
            System.Console.WriteLine("\nReceived request to create department...");
            ICreateOneDepartment creator = new CreateDepartment();
            creator.CreateOneDepartment(newDepartment);
        }

        // PUT: api/Departments/5
        //updates department name...currently not used
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Department updatedDepartment)
        {
            System.Console.WriteLine("\nReceived request to update department...");
            IUpdateOneDepartment updater = new UpdateDepartment();
            updater.UpdateOneDepartment(id, updatedDepartment);
        }

        // DELETE: api/Departments/5
        //removes department...currently not used
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            System.Console.WriteLine("\nReceived request to delete department...");
            IDeleteOne deleteTool = new DeleteDepartment();
            deleteTool.DeleteOne(id);
        }
    }
}
