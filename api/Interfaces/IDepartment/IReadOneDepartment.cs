using api.Models;

namespace api.Interfaces
{
    public interface IReadOneDepartment
    {
        public Department ReadOneDepartment(int id);
    }
}