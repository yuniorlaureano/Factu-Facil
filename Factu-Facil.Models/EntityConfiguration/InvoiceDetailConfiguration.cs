using FactuFacil.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactuFacil.Models.EntityConfiguration
{
    public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Product);
            builder.HasOne(p => p.Invoice)
                   .WithMany(p => p.InvoiceDetails);
            builder.HasOne(p => p.UpdatedBy);
            builder.HasOne(p => p.UpdatedBy);
        }
    }
}
