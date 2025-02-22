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

        public static async Task LoadCustomers(DataContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Customers.Any()) return;

            var json = File.ReadAllText("Data/json/customers.json");
            var customers = JsonSerializer.Deserialize<List<Customer>>(json, options);

            if (customers is not null && customers.Count > 0)
            {
                await context.Customers.AddRangeAsync(customers);
                await context.SaveChangesAsync();
            }
        }

        public static async Task LoadOrders(DataContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Orders.Any()) return;

            var json = File.ReadAllText("Data/json/orders.json");
            var orders = JsonSerializer.Deserialize<List<Order>>(json, options);

            if (orders is not null && orders.Count > 0)
            {
                await context.Orders.AddRangeAsync(orders);
                await context.SaveChangesAsync();
            }
        }

        public static async Task LoadOrderItems(DataContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.OrderItems.Any()) return;

            var json = File.ReadAllText("Data/json/orderitems.json");
            var orderItems = JsonSerializer.Deserialize<List<OrderItem>>(json, options);

            if (orderItems is not null && orderItems.Count > 0)
            {
                await context.OrderItems.AddRangeAsync(orderItems);
                await context.SaveChangesAsync();
            }
        }
    }
}
    