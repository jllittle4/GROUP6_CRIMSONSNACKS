using api.Models;
namespace api.Interfaces
{
    public interface IViewUsers
    {
        public List<User> GetAllUsers();
    }
}