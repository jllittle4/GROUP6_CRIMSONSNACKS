using api.Models;

namespace api.Interfaces
{
    public interface IUpdateOneUser
    {
        public void UpdateOneUser(int id, User myUser);
    }
}