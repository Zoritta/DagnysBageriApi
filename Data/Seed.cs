using DagnysBageriApi.Entities;
using DagnysBageriApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DagnysBageriApi
{
    public static class Seed
    {
        public static async Task LoadSuppliers(DataContext context)
        {
            if (!context.Suppliers.Any())
            {
                var suppliers = new List<Supplier>
                {
                    new Supplier { Name = "Supplier 1", Address = "Address 1", ContactPerson = "John Doe", PhoneNumber = "1234567890", Email = "john@example.com" },
                    new Supplier { Name = "Supplier 2", Address = "Address 2", ContactPerson = "Jane Doe", PhoneNumber = "0987654321", Email = "jane@example.com" }
                };

                await context.Suppliers.AddRangeAsync(suppliers);
                await context.SaveChangesAsync();
            }
        }

        public static async Task LoadRawMaterials(DataContext context)
        {
            if (!context.RawMaterials.Any())
            {
                var rawMaterials = new List<RawMaterial>
                {
                    new RawMaterial { ItemNumber = "RM001", Name = "Flour" },
                    new RawMaterial { ItemNumber = "RM002", Name = "Sugar" }
                };

                await context.RawMaterials.AddRangeAsync(rawMaterials);
                await context.SaveChangesAsync();
            }
        }

        public static async Task LoadSupplierMaterials(DataContext context)
        {
            if (!context.SupplierMaterials.Any())
            {
                var supplierMaterials = new List<SupplierMaterial>
                {
                    new SupplierMaterial { SupplierId = 1, RawMaterialId = 1, PricePerKg = 20.5m },
                    new SupplierMaterial { SupplierId = 2, RawMaterialId = 2, PricePerKg = 10.5m }
                };

                await context.SupplierMaterials.AddRangeAsync(supplierMaterials);
                await context.SaveChangesAsync();
            }
        }

        public static async Task LoadProducts(DataContext context)
        {
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product { Name = "Bread", Price = 50.0m, Weight = 1.0m, QuantityPerPack = 1, BestBeforeDate = DateTime.Now.AddDays(7), ManufactureDate = DateTime.Now },
                    new Product { Name = "Cake", Price = 30.0m, Weight = 0.5m, QuantityPerPack = 1, BestBeforeDate = DateTime.Now.AddDays(3), ManufactureDate = DateTime.Now }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }

        public static async Task LoadCustomers(DataContext context)
        {
            if (!context.Customers.Any())
            {
                var customers = new List<Customer>
                {
                    new Customer { StoreName = "Bakery A", ContactPerson = "Alice", PhoneNumber = "1234567890", Email = "alice@bakery.com", DeliveryAddress = "Address 1", InvoiceAddress = "Address 1" },
                    new Customer { StoreName = "Bakery B", ContactPerson = "Bob", PhoneNumber = "0987654321", Email = "bob@bakery.com", DeliveryAddress = "Address 2", InvoiceAddress = "Address 2" }
                };

                await context.Customers.AddRangeAsync(customers);
                await context.SaveChangesAsync();
            }
        }

        public static async Task LoadOrders(DataContext context)
        {
            if (!context.Orders.Any())
            {
                var orders = new List<Order>
                {
                    new Order { OrderDate = DateTime.Now, OrderNumber = "ORD001", CustomerId = 1 },
                    new Order { OrderDate = DateTime.Now, OrderNumber = "ORD002", CustomerId = 2 }
                };

                await context.Orders.AddRangeAsync(orders);
                await context.SaveChangesAsync();
            }
        }

        public static async Task LoadOrderItems(DataContext context)
        {
            if (!context.OrderItems.Any())
            {
                var orderItems = new List<OrderItem>
                {
                    new OrderItem { OrderId = 1, ProductId = 1, Quantity = 2, Price = 50.0m },
                    new OrderItem { OrderId = 2, ProductId = 2, Quantity = 1, Price = 30.0m }
                };

                await context.OrderItems.AddRangeAsync(orderItems);
                await context.SaveChangesAsync();
            }
        }
    }
}
