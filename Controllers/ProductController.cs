using DagnysBageriApi.Data;
using DagnysBageriApi.Entities;
using DagnysBageriApi.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DagnysBageriApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        // Add a new product
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] AddProductRequestModel request)
        {
            if (request == null)
            {
                return BadRequest(new { success = false, message = "Invalid product data." });
            }

            var product = new Product
            {
                Name = request.ProductName,
                Price = request.PricePerUnit,
                Weight = request.Weight,
                PackSize = request.QuantityPerPackage,
                BestBeforeDate = request.ExpirationDate,
                ManufactureDate = request.ManufacturingDate
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = $"Product '{product.Name}' added successfully." });
        }

        // List all products
        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            if (products == null || products.Count == 0)
            {
                return NotFound(new { success = false, message = "No products found." });
            }

            return Ok(new { success = true, products });
        }

        // Retrieve a specific product
        [HttpGet("{productId}")]
        public async Task<ActionResult> GetProduct(int productId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                return NotFound(new { success = false, message = $"Product with ID {productId} not found." });
            }

            return Ok(new { success = true, product });
        }

        // Update a product’s price for a specific supplier
        [HttpPut("{productId}/price")]
        public async Task<ActionResult> UpdateProductPrice(int productId, [FromBody] UpdatePriceRequestModel request)
        {
            if (request == null || request.NewPricePerKg <= 0)
            {
                return BadRequest(new { success = false, message = "Invalid price data." });
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                return NotFound(new { success = false, message = $"Product with ID {productId} not found." });
            }

            // Fetch the supplier's material entry
            var supplierMaterial = await _context.SupplierMaterials
                .Include(sm => sm.Supplier)
                .Include(sm => sm.RawMaterial)
                .FirstOrDefaultAsync(sm => sm.SupplierId == request.SupplierId &&
                                           sm.RawMaterial.ItemNumber == request.ItemNumber);

            if (supplierMaterial == null)
            {
                return NotFound(new { success = false, message = $"Supplier with ID {request.SupplierId} and material with item number {request.ItemNumber} not found." });
            }

            // Update the price per kilogram for the specific supplier's material
            supplierMaterial.PricePerKg = request.NewPricePerKg;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = $"Price updated for product '{product.Name}' under the specified supplier.",
                newPrice = supplierMaterial.PricePerKg
            });
        }
    }
}
