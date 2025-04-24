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
        public DbSet<Read> Read { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Read>()
                .HasOne(l => l.User)
                .WithMany(u => u.Reads)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
