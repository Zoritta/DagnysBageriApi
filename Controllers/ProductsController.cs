using DagnysBageriApi.Data;
using DagnysBageriApi.Entities;
using DagnysBageriApi.Models.RequestModels;
using DagnysBageriApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DagnysBageriApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;
        public ProductsController(DataContext context) => _context = context;

        // ---------- DTO used for all GET responses ------------------------
        private static ProductDto ToDto(Product p) => new()
        {
            ProductId = p.ProductId,
            Name = p.Name,
            Price = p.Price,
            Weight = p.Weight,
            QuantityPerPack = p.QuantityPerPack,
            BestBeforeDate = p.BestBeforeDate,
            ManufactureDate = p.ManufactureDate,
            ImageUrl = p.ImageUrl         // <-- NEW
        };

        // ---------- POST /api/products  ----------------------------------
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] AddProductRequestModel req)
        {
            if (req == null)
                return BadRequest(new { success = false, message = "Invalid product data." });

            var product = new Product
            {
                Name = req.ProductName,
                Price = req.PricePerUnit,
                Weight = req.Weight,
                QuantityPerPack = req.QuantityPerPackage,
                BestBeforeDate = req.ExpirationDate,
                ManufactureDate = req.ManufacturingDate,
                ImageUrl = req.ImageUrl           // accept an image URL
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById),
                                   new { id = product.ProductId },
                                   ToDto(product));
        }

        // ---------- GET /api/products  -----------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            if (!products.Any())
                return NotFound(new { success = false, message = "No products found." });

            return Ok(products.Select(ToDto));
        }

        // ---------- GET /api/products/{id}  ------------------------------
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(new { success = false, message = "Product not found." });

            return Ok(ToDto(product));
        }

        // ---------- GET /api/products/name/{name}  -----------------------
        [HttpGet("name/{productName}")]
        public async Task<ActionResult<ProductDto>> GetProductByName(string productName)
        {
            var product = await _context.Products
                                        .FirstOrDefaultAsync(p =>
                                             p.Name.ToLower() == productName.Trim().ToLower());

            if (product == null)
                return NotFound(new { success = false, message = $"Product '{productName}' not found." });

            return Ok(ToDto(product));
        }

        // ---------- PUT /api/products/name/{name}/price  -----------------
        [HttpPut("name/{productName}/price")]
        public async Task<ActionResult> UpdateProductPriceByName
            (string productName, [FromBody] UpdatePriceRequestModel req)
        {
            if (req == null || req.NewPrice <= 0)
                return BadRequest(new { success = false, message = "Invalid price data." });

            var product = await _context.Products
                                        .FirstOrDefaultAsync(p =>
                                             p.Name.Replace(" ", "").ToLower() ==
                                             productName.Replace(" ", "").ToLower());

            if (product == null)
                return NotFound(new { success = false, message = $"Product '{productName}' not found." });

            product.Price = req.NewPrice;
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Price updated.", newPrice = product.Price });
        }
    }
}