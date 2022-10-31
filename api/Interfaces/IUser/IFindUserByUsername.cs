using api.Models;

namespace api.Interfaces
{
    public interface IFindUserByUsername
    {
        public User Find(string username);
    }
}