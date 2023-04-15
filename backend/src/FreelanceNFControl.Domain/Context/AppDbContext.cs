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

        public AppDbContext()
        {
        }

        public override DbSet<User> Users { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
    }
}
