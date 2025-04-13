using Microsoft.EntityFrameworkCore;
using simpleReading.Context;
using simpleReading.Models;

namespace simpleReading.Service
{
    public class AuthService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<User?> Login(string email, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(predicate: x => x.Email == email && x.Password == password);
            if (user is not null)
                return user;

            return null;
        }

        public async Task Register(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
