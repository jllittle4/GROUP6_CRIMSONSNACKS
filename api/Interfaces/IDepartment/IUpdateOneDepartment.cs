using api.Models;

namespace api.Interfaces
{
    public interface IUpdateOneDepartment
    {
        public void UpdateOneDepartment(int id, Department myDepartment);
    }
}