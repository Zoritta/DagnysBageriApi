

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
        [HttpGet("materials/{id}")]
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
    }
}