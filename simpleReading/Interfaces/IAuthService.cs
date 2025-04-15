using simpleReading.Models;
using System.Threading.Tasks;

namespace simpleReading.Interfaces
{
    public interface IAuthService
    {
        Task<User?> Login(string email, string password);
        Task<bool> Register(User user);
    }
}
