﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using simpleReading.Context;
using simpleReading.Interfaces;
using simpleReading.Models;
using simpleReading.ViewModel;
using Sprache;

namespace simpleReading.Services
{
    public class ReadService(
        AppDbContext context,
        IUserService userService
        ) : IReadService
    {
        private readonly AppDbContext _context = context;
        private readonly IUserService _userService = userService;

        [HttpPost]
        public async Task<bool> Create(Read read, string userId)
        {
            if (read.CreatedAt == DateTime.MinValue)
                read.CreatedAt = DateTime.UtcNow;
            else
                read.CreatedAt = read.CreatedAt.ToUniversalTime();

                read.UserId = userId;

            await _context.AddAsync(read);
            await _context.SaveChangesAsync();

            return true;
        }

        [HttpPost]
        public async Task<bool> CreateWithReadedPages(Read updateRead, string userId)
        {
            var read = await _context.Read.FirstOrDefaultAsync(x => x.Id == updateRead.Id);

            if (updateRead.Finished)
            {
                await UpdateFinished(updateRead.Id, updateRead.Finished);
            }

            updateRead.Id = Guid.NewGuid().ToString();
            updateRead.UserId = userId;
            updateRead.Finished = true;

            if (updateRead.CreatedAt == DateTime.MinValue)
                updateRead.CreatedAt = DateTime.UtcNow;
            else
                updateRead.CreatedAt = DateTime.UtcNow;

            await _context.AddAsync(updateRead);
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
            var reads = await _context.Read
                .Where(u => u.UserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            if (reads == null) return null;

            return reads;
        }

        public async Task<bool> UpdateFinished(string readId, bool finished)
        {
            var read = await _context.Read.FirstOrDefaultAsync(x => x.Id == readId);

            read.Finished = finished;

            _context.Read.Update(read);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ReadOperationResult> Update(Read input)
        {
            var read = await _context.Read.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (read == null)
                return new ReadOperationResult(false, "leitura não encontrada");

            read.Title = input.Title;
            read.Author = input.Author;
            read.Source = input.Source;
            read.Finished = input.Finished;
            read.Pages = input.Pages;

            _context.Read.Update(read);
            await _context.SaveChangesAsync();

            return new ReadOperationResult(true, "leitura atualizada com sucesso", read);
        }

        public async Task<User> UpdateCurrentUserReads(User user)
        {
            var newReads = await GetReadsByUserId(user.Id);

            user.Reads = newReads;

            return user;
        }

        public async Task<List<Read>?> GetReadsByUsername(string username)
        {
            var user = await _userService.GetUserByUsername(username);
            if (user == null) return null;

            user.Reads = await GetReadsByUserId(user.Id);

            return user.Reads.ToList();
        }

        public GroupedReadsViewModel MountViewModel(ICollection<Read> reads)
        {
            var groupedReads = new GroupedReadsViewModel
            {
                ReadsByYearAndMonth = reads
                .GroupBy(r => r.CreatedAt.Year)
                .ToDictionary(
                    yearGroup => yearGroup.Key,
                    yearGroup => yearGroup
                    .GroupBy(r => r.CreatedAt.Month)
                    .ToDictionary(
                        monthGroup => monthGroup.Key,
                        monthGroup => monthGroup.ToList()
                    )
                )
            };

            return groupedReads;
        }
    }
}
