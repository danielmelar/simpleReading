using simpleReading.Models;
using simpleReading.ViewModel;

namespace simpleReading.Interfaces
{
    public interface IReadService
    {
        Task<ICollection<Read>?> GetReadsByUserId(string userId);
        Task<bool> Create(Read read, string userId);
        Task<bool> CreateWithReadedPages(Read updateRead, string userId);
        Task<bool> Delete(string id);
        Task<ReadOperationResult> Update(Read uRead);
        Task<User> UpdateCurrentUserReads(User user);
        Task<List<Read>> GetReadsByUsername(string username);
        GroupedReadsViewModel MountViewModel(ICollection<Read> reads);
    }
}
