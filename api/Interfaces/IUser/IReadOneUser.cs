using api.Models;

namespace api.Interfaces
{
    public interface IReadOneUser
    {
        public User ReadOneUser(string searchVal);
    }
}