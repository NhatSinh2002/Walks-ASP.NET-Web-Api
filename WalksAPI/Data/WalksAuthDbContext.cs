using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WalksAPI.Data
{
    public class WalksAuthDbContext : IdentityDbContext
    {
        public WalksAuthDbContext(DbContextOptions<WalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "a77aa48b-5c69-4b26-972f-f8d118cfe465";
            var writerRoleId = "ab07c305-095b-4259-95f3-3d1beb1fff99";
            var role = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id= readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id= writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(role);
        }
    }
}
