using simpleReading.Models;

namespace simpleReading.Interfaces
{
    public interface IReadService
    {
        Task<ICollection<Read>?> GetReadsByUserId(string userId);
    }
}
