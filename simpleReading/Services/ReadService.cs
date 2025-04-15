using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<bool> Create(Read read, string userId)
        {
            read.UserId = userId;

            await _context.AddAsync(read);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(string id)
        {
            var read = await _context.Read.FirstAsync(x => x.Id == id);

            _context.Read.Remove(read);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<Read>?> GetReadsByUserId(string userId)
        {
            var AllReads = await _context.Read.ToListAsync();

            var userReads = AllReads.FindAll(x => x.UserId == userId);
            if (userReads == null) return null;

            return userReads;
        }

        public async Task<Read> Update(Read uRead)
        {
            var read = await _context.Read.FirstAsync(x => x.Id == uRead.Id);

            _context.Read.Update(read);
            await _context.SaveChangesAsync();

            return read;
        }

        public async Task<User> UpdateCurrentUserReads(User user)
        {
            var newReads = await GetReadsByUserId(user.Id);

            user.Reads = newReads;

            return user;
        }
    }
}
