using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using simpleReading.Context;
using simpleReading.Interfaces;
using simpleReading.Models;

namespace simpleReading.Services
{
    public class UserService(
        AppDbContext context
        ) : IUserService
    {
        private readonly AppDbContext _context = context;

        public async Task<User?> GetUserByLoginCredentials(string email, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(predicate: x => x.Email == email && x.Password == password);
            if (user == null)
                return null;

            return user;
        }

        //public async Task<User> GetUser()
        //{

        //}

        public async Task CreateUser()
        {

        }
    }
}
