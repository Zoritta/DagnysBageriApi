using DagnysBageriApi.Data;
using DagnysBageriApi.Entities;
using DagnysBageriApi.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DagnysBageriApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

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
                QuantityPerPack = request.QuantityPerPackage,
                BestBeforeDate = request.ExpirationDate,
                ManufactureDate = request.ManufacturingDate
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = $"Product '{product.Name}' added successfully." });
        }

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

        [HttpGet("name/{productName}")]
        public async Task<ActionResult> GetProductByName(string productName)
        {
            productName = productName.Trim().ToLower();

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Name.ToLower() == productName);

            if (product == null)
            {
                return NotFound(new { success = false, message = $"Product '{productName}' not found." });
            }

            return Ok(new { success = true, product });
        }

        [HttpPut("name/{productName}/price")]
        public async Task<ActionResult> UpdateProductPriceByName(string productName, [FromBody] UpdatePriceRequestModel request)
        {
            if (request == null || request.NewPrice <= 0)
            {
                return BadRequest(new { success = false, message = "Invalid price data." });
            }

            productName = productName.Trim().Replace(" ", "").ToLower(); ;

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Name.Trim().Replace(" ", "").ToLower() == productName);

            if (product == null)
            {
                return NotFound(new { success = false, message = $"Product '{productName}' not found." });
            }

            product.Price = request.NewPrice;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = $"Price for product '{product.Name}' has been updated.",
                newPrice = product.Price
            });
        }

    }
}
