using System;
using System.Collections.Generic;
using System.Text;
using FactuFacil.Models.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace FactuFacil.Entity
{
    public class FactuFacilContext : DbContext
    {
        public FactuFacilContext(DbContextOptions<FactuFacilContext> dbContextOptions) : base(dbContextOptions)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Inventory> Inventorie { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetail { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<User> User { get; set; }
    }
}
