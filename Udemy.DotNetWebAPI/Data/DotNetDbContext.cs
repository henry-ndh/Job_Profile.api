using Microsoft.EntityFrameworkCore;
using Udemy.DotNetWebAPI.Models.Domain;

namespace Udemy.DotNetWebAPI.Data
{
    public class DotNetDbContext : DbContext
    {
        public DotNetDbContext(DbContextOptions<DotNetDbContext> options) : base(options)
        {
        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
