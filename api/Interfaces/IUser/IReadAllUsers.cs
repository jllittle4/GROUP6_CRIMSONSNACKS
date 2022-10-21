using api.Models;

namespace api.Interfaces
{
    public interface IReadAllUsers
    {
        public List<User> ReadAllUsers();
    }
}