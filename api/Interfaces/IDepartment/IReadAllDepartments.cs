using api.Models;

namespace api.Interfaces
{
    public interface IReadAllDepartments
    {
        public List<Department> ReadAllDepartments();
    }
}