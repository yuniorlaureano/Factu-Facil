using FactuFacil.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactuFacil.Models.EntityConfiguration
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Client);
            builder.HasMany(p => p.InvoiceDetails)
                    .WithOne(p => p.Invoice)
                    .HasForeignKey(p => p.InvoiceId);
            builder.HasOne(p => p.UpdatedBy);
            builder.HasOne(p => p.UpdatedBy);
        }
    }
}
