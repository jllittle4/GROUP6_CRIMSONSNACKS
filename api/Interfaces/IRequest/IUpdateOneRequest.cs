using api.Models;

namespace api.Interfaces
{
    public interface IUpdateOneRequest
    {
        public void UpdateOneRequest(int id, Request myRequest);
    }
}