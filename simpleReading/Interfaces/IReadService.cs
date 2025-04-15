using simpleReading.Models;

namespace simpleReading.Interfaces
{
    public interface IReadService
    {
        Task<ICollection<Read>?> GetReadsByUserId(string userId);
        Task<bool> Create(Read read, string userId);
        Task<bool> Delete(string id);
        Task<Read> Update(Read uRead);
        Task<User> UpdateCurrentUserReads(User user);
    }
}
