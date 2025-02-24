using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks_API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }
            protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleID = "cc7a3b1d-242e-4f0b-bd01-2f3930ca1317";
            var writerRoleID = "eba7b58d-3863-46b6-ab27-fe6f92431216";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleID,
                    ConcurrencyStamp = readerRoleID,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleID,
                    ConcurrencyStamp = writerRoleID,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
    

