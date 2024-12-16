using DagnysBageriApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DagnysBageriApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<SupplierMaterial> SupplierMaterials { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SupplierMaterial>().HasKey(sm => new { sm.SupplierId, sm.RawMaterialId });

            modelBuilder.Entity<SupplierMaterial>()
                .HasOne(sm => sm.Supplier)
                .WithMany(s => s.SupplierMaterials)
                .HasForeignKey(sm => sm.SupplierId);

            modelBuilder.Entity<SupplierMaterial>()
                .HasOne(sm => sm.RawMaterial)
                .WithMany(rm => rm.SupplierMaterials)
                .HasForeignKey(sm => sm.RawMaterialId);

        }

    }
}