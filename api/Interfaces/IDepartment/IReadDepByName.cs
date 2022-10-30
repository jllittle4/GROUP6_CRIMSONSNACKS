using api.Models;

namespace api.Interfaces
{
    public interface IReadDepByName
    {
        public Department Find(string depname);
    }
}