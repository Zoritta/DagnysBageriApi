using System.Text.Json;
using DagnysBageriApi.Entities;
namespace DagnysBageriApi.Data
{
    public static class Seed
    {
        public static async Task LoadSuppliers(DataContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Suppliers.Any()) return;

            var json = File.ReadAllText("Data/json/suppliers.json");
            var suppliers = JsonSerializer.Deserialize<List<Supplier>>(json, options);

            if (suppliers is not null && suppliers.Count > 0)
            {
                await context.Suppliers.AddRangeAsync(suppliers);
                await context.SaveChangesAsync();
            }
        }

        public static async Task LoadRawMaterials(DataContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.RawMaterials.Any()) return;

            var json = File.ReadAllText("Data/json/rawmaterials.json");
            var rawMaterials = JsonSerializer.Deserialize<List<RawMaterial>>(json, options);

            if (rawMaterials is not null && rawMaterials.Count > 0)
            {
                await context.RawMaterials.AddRangeAsync(rawMaterials);
                await context.SaveChangesAsync();
            }
        }

        public static async Task LoadSupplierMaterials(DataContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.SupplierMaterials.Any()) return;

            var json = File.ReadAllText("Data/json/suppliermaterials.json");
            var supplierMaterials = JsonSerializer.Deserialize<List<SupplierMaterial>>(json, options);

            if (supplierMaterials is not null && supplierMaterials.Count > 0)
            {
                await context.SupplierMaterials.AddRangeAsync(supplierMaterials);
                await context.SaveChangesAsync();
            }
        }

        public static async Task LoadProducts(DataContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Products.Any()) return;

            var json = File.ReadAllText("Data/json/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(json, options);

            if (products is not null && products.Count > 0)
            {
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
    