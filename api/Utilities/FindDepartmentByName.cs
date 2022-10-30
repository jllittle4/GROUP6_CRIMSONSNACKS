using api.Controllers;
using api.Interfaces;
using api.Models;
using api.CRUD;

namespace api.Utilities
{
    
    public class FindDepartmentByName
    {
        public Department myDep { get; set; }

        public int Find(string departmentname)
        {
            System.Console.WriteLine("\nLooking for department by name...");

            IReadAllDepartments allUsers = new ReadDepartments();
            List<Department> myList = allUsers.ReadAllDepartments();

            myDep = myList.Find(x => x.DepName == departmentname);

            try
            {
                System.Console.WriteLine("Found department.");
                return myDep.DepId;

            }
            catch
            {
                System.Console.WriteLine("Could not find department.");
            }

            return -1;
        }
    }
}