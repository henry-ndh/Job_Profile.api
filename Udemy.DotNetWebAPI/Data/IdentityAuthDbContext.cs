using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Udemy.DotNetWebAPI.Data
{
    public class IdentityAuthDbContext : IdentityDbContext
    {
        public IdentityAuthDbContext(DbContextOptions<IdentityAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "66cca578-d8b9-40ed-9200-564c695532b5";
            var writerRoleId = "ff494ae7-528a-4890-8258-48b0b3404416";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER",
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "WRITER",
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
