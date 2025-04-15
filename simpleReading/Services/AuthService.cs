using Microsoft.EntityFrameworkCore;
using simpleReading.Context;
using simpleReading.Interfaces;
using simpleReading.Models;

namespace simpleReading.Services
{
    public class AuthService(
        AppDbContext context,
        ILogger<AuthService> logger,
        IUserService userService,
        IReadService readService
        ) : IAuthService
    {
        private readonly AppDbContext _context = context;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly IUserService _userService = userService;
        private readonly IReadService _readService = readService;

        public async Task<User?> Login(string email, string password)
        {
            var user = await _userService.GetUserByLoginCredentials(email, password);
            if (user == null) return null;

            user.Reads = await _readService.GetReadsByUserId(user.Id);

            return user;
        }

        public async Task<bool> Register(User user)
        {
            try
            {
                await _context.User.AddAsync(user);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
