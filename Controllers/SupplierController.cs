
using System.Reflection.Metadata.Ecma335;
using DagnysBageriApi.Data;
using DagnysBageriApi.Entities;
using DagnysBageriApi.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DagnysBageriApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet("{supplierName}/products")]
        public async Task<IActionResult> GetSupplierProducts(string supplierName)
        {
            supplierName = supplierName.Trim();
            var supplier = await _context.Suppliers
                .Include(s => s.SupplierMaterials)
                .ThenInclude(sm => sm.RawMaterial)
                .FirstOrDefaultAsync(s => s.Name.ToLower().Replace(" ", "") == supplierName.ToLower());

            if (supplier == null)
            {
                return NotFound(new { success = false, message = $"Supplier '{supplierName}' not found." });
            }

            var products = supplier.SupplierMaterials.Select(sm => new
            {
                ProductName = sm.RawMaterial.Name,
                ItemNumber = sm.RawMaterial.ItemNumber,
                PricePerKg = sm.PricePerKg
            });

            return Ok(new { success = true, supplierName = supplier.Name, products });
        }

        [HttpPost("{supplierName}/products")]
        public async Task<ActionResult> AddProductToSupplier(string supplierName, [FromBody] AddMaterialToSupplierRequest request)
        {
            supplierName = supplierName.Trim();
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(s =>
            s.Name.ToLower().Replace(" ", "") == supplierName.ToLower());
            if (supplier == null)
            {
                return NotFound(new { success = false, message = $"Supplier '{supplierName}' not found!"});
            }
            if(await _context.SupplierMaterials.AnyAsync(sm => 
            sm.SupplierId == supplier.SupplierId &&
            sm.RawMaterial.ItemNumber == request.ItemNumber))
            {
                return BadRequest(new {success = false, message = $" Product '{request.ItemNumber}' already exists for '{supplierName}'!"});
            }

            var rawMaterial = await _context.RawMaterials
                .FirstOrDefaultAsync(rm => rm.ItemNumber == request.ItemNumber);

            if (rawMaterial == null)
            {
                rawMaterial = new RawMaterial
                {
                    ItemNumber = request.ItemNumber,
                    Name = request.Name
                };
            }
            var supplierMaterial = new SupplierMaterial
            {
                SupplierId = supplier.SupplierId,
                RawMaterial = rawMaterial,
                PricePerKg = request.PricePerKg
            };
            _context.SupplierMaterials.Add(supplierMaterial);
            await _context.SaveChangesAsync();

            return Ok(new {success = true, message = $"Product '{request.Name}' added to supplier '{supplierName}'."});
        }


    }
}
