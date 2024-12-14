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
            modelBuilder.Entity<SupplierMaterial>().HasKey(o => new { o.SupplierId, o.RawMaterialId});
        }
    }
}