using simpleReading.Models;

namespace simpleReading.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByLoginCredentials(string email, string password);
    }
}
