using DagnysBageriApi.Entities;
using DagnysBageriApi.Data;
using System.Text.Json;

namespace DagnysBageriApi
{
    public static class Seed
    {
        public static async Task LoadSuppliers(DataContext context)
        {
            if (!context.Suppliers.Any())
            {
                var suppliers = await LoadFromJsonAsync<List<Supplier>>("Data/json/suppliers.json");
                if (suppliers != null)
                {
                    await context.Suppliers.AddRangeAsync(suppliers);
                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task LoadRawMaterials(DataContext context)
        {
            if (!context.RawMaterials.Any())
            {
                var rawMaterials = await LoadFromJsonAsync<List<RawMaterial>>("Data/json/rawMaterials.json");
                if (rawMaterials != null)
                {
                    await context.RawMaterials.AddRangeAsync(rawMaterials);
                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task LoadSupplierMaterials(DataContext context)
        {
            if (!context.SupplierMaterials.Any())
            {
                var supplierMaterials = await LoadFromJsonAsync<List<SupplierMaterial>>("Data/json/supplierMaterials.json");

                if (supplierMaterials != null && supplierMaterials.Any())
                {
                    await context.SupplierMaterials.AddRangeAsync(supplierMaterials);
                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task LoadProducts(DataContext context)
        {
            if (!context.Products.Any())
            {
                var products = await LoadFromJsonAsync<List<Product>>("Data/json/products.json"); 

                if (products != null && products.Any())
                {
                    await context.Products.AddRangeAsync(products);
                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task LoadCustomers(DataContext context)
        {
            if (!context.Customers.Any())
            {
                var customers = await LoadFromJsonAsync<List<Customer>>("Data/json/customers.json"); 
                if (customers != null && customers.Any())
                {
                    await context.Customers.AddRangeAsync(customers);
                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task LoadOrders(DataContext context)
        {
            if (!context.Orders.Any())
            {
                var orders = await LoadFromJsonAsync<List<Order>>("Data/json/orders.json");
                if (orders != null && orders.Any())
                {
                    await context.Orders.AddRangeAsync(orders);
                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task LoadOrderItems(DataContext context)
        {
            if (!context.OrderItems.Any())
            {
                var orderItems = await LoadFromJsonAsync<List<OrderItem>>("Data/json/orderItems.json");
                if (orderItems != null && orderItems.Any())
                {
                    await context.OrderItems.AddRangeAsync(orderItems);
                    await context.SaveChangesAsync();
                }
            }
        }
        private static async Task<T?> LoadFromJsonAsync<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return default;
            }

            try
            {
                var json = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file {filePath}: {ex.Message}");
                return default;
            }
        }
    }
}
