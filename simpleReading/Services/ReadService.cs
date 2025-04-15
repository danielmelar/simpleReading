using Microsoft.EntityFrameworkCore;
using simpleReading.Context;
using simpleReading.Interfaces;
using simpleReading.Models;

namespace simpleReading.Services
{
    public class ReadService(
        AppDbContext context
        ) : IReadService
    {
        private readonly AppDbContext _context = context;
        public async Task<ICollection<Read>?> GetReadsByUserId(string userId)
        {
            var AllReads = await _context.Read.ToListAsync();

            var userReads = AllReads.FindAll(x => x.UserId == userId);
            if (userReads == null) return null;

            return userReads;
        }
    }
}
