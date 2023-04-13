using FreelanceNFControl.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreelanceNFControl.Domain.DbContext
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public override DbSet<User> Users { get; set; }
    }
}
