using DagnysBageriApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DagnysBageriApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<SupplierMaterial> SupplierMaterials { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite Key for SupplierMaterial
            modelBuilder.Entity<SupplierMaterial>()
                .HasKey(sm => new { sm.SupplierId, sm.RawMaterialId });

            modelBuilder.Entity<SupplierMaterial>()
                .HasOne(sm => sm.Supplier)
                .WithMany(s => s.SupplierMaterials)
                .HasForeignKey(sm => sm.SupplierId);

            modelBuilder.Entity<SupplierMaterial>()
                .HasOne(sm => sm.RawMaterial)
                .WithMany(rm => rm.SupplierMaterials)
                .HasForeignKey(sm => sm.RawMaterialId);

            // Composite Key for OrderItem (Many-to-Many Relationship)
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId });

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);
        }
    }
}
