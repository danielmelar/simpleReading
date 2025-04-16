using simpleReading.Models;

namespace simpleReading.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticationResult> GetUserByLoginCredentials(string email, string password);
        Task<AuthenticationResult> Create(User input);
    }
}
