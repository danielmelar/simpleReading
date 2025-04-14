using Microsoft.EntityFrameworkCore;
using simpleReading.Models;

namespace simpleReading.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> User { get; set; }
    }
}
