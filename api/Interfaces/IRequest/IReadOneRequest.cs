using api.Models;

namespace api.Interfaces
{
    public interface IReadOneRequest
    {
        public Request ReadOneRequest(int id);
    }
}