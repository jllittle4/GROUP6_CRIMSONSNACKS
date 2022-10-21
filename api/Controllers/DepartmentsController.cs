using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Interfaces;
using api.CRUD;
using Microsoft.AspNetCore.Cors;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Departments : ControllerBase
    {
        // GET: api/Departments
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<Department> Get()
        {
            System.Console.WriteLine("\nReceived request to get all departments...");

            IReadAllDepartments readerAll = new ReadDepartments();

            return readerAll.ReadAllDepartments();
        }

        // GET: api/Departments/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetDepartments")]
        //may have issues finding by departmentname as opposed to departmentid
        public Department Get([FromBody] string departmentName)
        {
            System.Console.WriteLine("\nReceived request to find department...");
            
            IReadOneDepartment readerOne = new ReadDepartments();
            
            return readerOne.ReadOneDepartment(departmentName);
        }

        // POST: api/Departments
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] Department newDepartment)
        {
            System.Console.WriteLine("\nReceived request to create department...");
            //System.Console.WriteLine(newDepartment.ToString());

            ICreateOneDepartment creator = new CreateDepartment();
            creator.CreateOneDepartment(newDepartment);
        }

        // PUT: api/Departments/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Department updatedDepartment)
        {
            System.Console.WriteLine("\nReceived request to update department...");
            //System.Console.WriteLine(updatedDepartment.ToString());

            IUpdateOneDepartment updater = new UpdateDepartment();
            updater.UpdateOneDepartment(id, updatedDepartment);
        }

        // DELETE: api/Departments/5
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
