
using DagnysBageriApi.Data;
using DagnysBageriApi.Entities;
using DagnysBageriApi.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DagnysBageriApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet()]
        public async Task<ActionResult<List<Supplier>>> GetSuppliersWithRawMaterials()
        {
            var suppliers = await _context.Suppliers
                .Include(s => s.SupplierMaterials)
                    .ThenInclude(sm => sm.RawMaterial)
                .ToListAsync();

            return Ok(suppliers);
        }

        [HttpPost]
        public async Task<ActionResult> AddSupplier([FromBody] Supplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.Name))
            {
                return BadRequest(new { success = false, message = "Name is required." });
            }

            if (await _context.Suppliers.AnyAsync(s => s.Name.ToLower() == supplier.Name.ToLower()))
            {
                return Conflict(new { success = false, message = $"Supplier '{supplier.Name}' already exists." });
            }

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSuppliersWithRawMaterials), new { id = supplier.SupplierId }, new { success = true, supplierId = supplier.SupplierId });
        }

        [HttpGet("{supplierName}/materials")]
        public async Task<IActionResult> GetSupplierMaterials(string supplierName)
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

            var rawMaterials = supplier.SupplierMaterials.Select(sm => new
            {
                ProductName = sm.RawMaterial.Name,
                ItemNumber = sm.RawMaterial.ItemNumber,
                PricePerKg = sm.PricePerKg
            });

            return Ok(new { success = true, supplierName = supplier.Name, rawMaterials });
        }

        [HttpPost("{supplierName}/materials")]
        public async Task<ActionResult> AddMaterialToSupplier(string supplierName, [FromBody] AddMaterialToSupplierRequestModel request)
        {
            supplierName = supplierName.Trim().ToLower();
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(s =>
            s.Name.ToLower().Replace(" ", "") == supplierName);
            if (supplier == null)
            {
                return NotFound(new { success = false, message = $"Supplier '{supplierName}' not found!" });
            }
            if (await _context.SupplierMaterials.AnyAsync(sm =>
            sm.SupplierId == supplier.SupplierId &&
            sm.RawMaterial.ItemNumber == request.ItemNumber))
            {
                return BadRequest(new { success = false, message = $" Product '{request.ItemNumber}' already exists for '{supplierName}'!" });
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

            return Ok(new { success = true, message = $"Product '{request.Name}' added to supplier '{supplierName}'." });
        }

        [HttpPatch("{supplierName}/materials/{itemNumber}/price")]
        public async Task<ActionResult> UpdateMaterialPrice(string supplierName, string itemNumber, [FromBody] UpdatePriceRequestModel request)
        {
            supplierName = supplierName.Trim().ToLower();
            itemNumber = itemNumber.Trim().ToLower();
            var supplierMaterial = await _context.SupplierMaterials
                .Include(sm => sm.Supplier)
                .Include(sm => sm.RawMaterial)
                .FirstOrDefaultAsync(sm =>
                    sm.Supplier.Name.ToLower().Replace(" ", "") == supplierName &&
                    sm.RawMaterial.ItemNumber.ToLower().Replace(" ", "") == itemNumber);



            if (supplierMaterial == null)
            {
                return NotFound(new { success = false, message = $"We could not find material'{itemNumber}' for supplier '{supplierName}'!" });
            }

            supplierMaterial.PricePerKg = request.NewPrice;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = $"Price updated for product '{supplierMaterial.RawMaterial.Name}' under requested supplier.",
                newPrice = supplierMaterial.PricePerKg
            });
        }
    }

}

