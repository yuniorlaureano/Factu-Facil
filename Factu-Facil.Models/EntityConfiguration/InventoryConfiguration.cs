using FactuFacil.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactuFacil.Models.EntityConfiguration
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Product);
            builder.HasOne(p => p.UpdatedBy);
            builder.HasOne(p => p.UpdatedBy);
        }
    }
}
