

using DagnysBageriApi.Data;
using DagnysBageriApi.Entities;
using DagnysBageriApi.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DagnysBageriApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RawMaterialsController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet()]
        public async Task<ActionResult> ListAllMaterials()
        {
            var materials = await _context.RawMaterials
            .Include(sm => sm.SupplierMaterials)
            .Select(rawMaterial => new {
                rawMaterial.RawMaterialId,
                rawMaterial.ItemNumber,
                rawMaterial.Name,
                Suppliers = rawMaterial.SupplierMaterials
                .Select(s => new {
                    s.Supplier.Name,
                    s.PricePerKg
                })
            })
            .ToListAsync();
            return Ok(new { success = true, StatusCode = 200, data = materials});
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> FindMaterial(int id)
        {
            var material = await _context.RawMaterials
            .Where(m => m.RawMaterialId == id)
            .Include(sm => sm.SupplierMaterials)
            .Select(rawMaterial => new
            {
                rawMaterial.RawMaterialId,
                rawMaterial.ItemNumber,
                rawMaterial.Name,
                Suppliers = rawMaterial.SupplierMaterials
                .Select(s => new
                {
                    s.Supplier.Name,
                    s.PricePerKg
                })
            })
            .SingleOrDefaultAsync();
            if (material is null)
            {
                return NotFound(new {success = false, StatusCode = 404, message = $"Unfortunately we could not find any materials with id {id}"});
            }
            return Ok(new { success = true,  data = material });
        }
        [HttpPost("add-to-supplier")]
        public async Task<ActionResult> AddMaterialToSupplier(AddMaterialToSupplierRequest request)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => EF.Functions.Like(s.Name, request.SupplierName));
            if(supplier == null)
            {
                return NotFound(new {success = false, message = "Supplier not found."});
            }
            if (await _context.SupplierMaterials.AnyAsync(sm => sm.SupplierId == supplier.SupplierId &&
            sm.RawMaterial.ItemNumber == request.ItemNumber))
            {
                return BadRequest(new {success = false, StatusCode = 404, message = $"This material already exists for the Supplier{supplier.Name}"});
            }
            var rawMaterial = new RawMaterial
            {
                ItemNumber = request.ItemNumber,
                Name = request.Name
            };
            var supplierMaterial = new SupplierMaterial
            {
                SupplierId = supplier.SupplierId,
                RawMaterial = rawMaterial,
                PricePerKg = request.PricePerKg
            };
            _context.SupplierMaterials.Add(supplierMaterial);
            await _context.SaveChangesAsync();     
            return Ok(new {success = true, message = $"Material {request.Name} added to supplier {request.SupplierName}"});

        }
        // [HttpPut("{update-price}")]
        // public async Task<ActionResult> UpdatePrice(UpdatePriceRequest request)
        // {
        //     var supplierMaterial = await _context.SupplierMaterials
        //     .Include(sm => sm.RawMaterial)
        //     .FirstOrDefaultAsync(sm =>
        //         sm.SupplierId == request.SupplierId && sm.RawMaterial.ItemNumber == request.ItemNumber);
        //     if ()
        //     return Ok();
        // }
        // [HttpDelete()]
        // public async Task<ActionResult> DeleteRawMaterial(int id)
        // {
        //     return Ok();
        // }
    }
}