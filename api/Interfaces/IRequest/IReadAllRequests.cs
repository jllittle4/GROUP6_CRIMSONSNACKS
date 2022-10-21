using api.Models;

namespace api.Interfaces
{
    public interface IReadAllRequests
    {
        public List<Request> ReadAllRequests();
    }
}