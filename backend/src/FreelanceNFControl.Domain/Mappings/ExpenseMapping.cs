using FreelanceNFControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelanceNFControl.Domain.Mappings
{
    public class ExpenseMapping : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expense", "system");

            builder.HasKey(expense => expense.Id);

            builder
                .HasOne(expense => expense.User)
                .WithMany(expense => expense.Expenses)
                .HasForeignKey(expense => expense.UserId);
        }
    }
}
