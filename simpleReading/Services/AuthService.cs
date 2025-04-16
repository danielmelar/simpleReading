using Microsoft.EntityFrameworkCore;
using simpleReading.Context;
using simpleReading.Interfaces;
using simpleReading.Models;

namespace simpleReading.Services
{
    public class AuthService(
        IUserService userService,
        IReadService readService
        ) : IAuthService
    {
        private readonly IUserService _userService = userService;
        private readonly IReadService _readService = readService;

        public async Task<AuthenticationResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email)
                || string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
                return new AuthenticationResult(false, "Preencha os campos vazios!");

            var result = await _userService.GetUserByLoginCredentials(email, password);
            if (!result.Sucess) return result;

            result.User.Reads = await _readService.GetReadsByUserId(result.User.Id);

            return result;
        }

        public async Task<AuthenticationResult> Register(User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrWhiteSpace(user.Username)
                || string.IsNullOrEmpty(user.Email) || string.IsNullOrWhiteSpace(user.Email)
                || string.IsNullOrEmpty(user.Password) || string.IsNullOrWhiteSpace(user.Password))
                return new AuthenticationResult(false, "Preencha os campos vazios!");

            var result = await _userService.Create(user);
            if (!result.Sucess) return result;

            return result;
        }
    }
}
