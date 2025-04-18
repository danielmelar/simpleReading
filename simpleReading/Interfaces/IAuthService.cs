using simpleReading.Models;
using System.Threading.Tasks;

namespace simpleReading.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResult> Login(string email, string password);
        Task<AuthenticationResult> Register(User user);
    }
}
