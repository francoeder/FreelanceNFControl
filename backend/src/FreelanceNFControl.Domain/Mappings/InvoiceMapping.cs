using FreelanceNFControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelanceNFControl.Domain.Mappings
{
    public class InvoiceMapping : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoice");

            builder.HasKey(invoice => invoice.Id);

            builder
                .HasOne(invoice => invoice.User)
                .WithMany(user => user.Invoices)
                .HasForeignKey(invoice => invoice.UserId);
        }
    }
}
