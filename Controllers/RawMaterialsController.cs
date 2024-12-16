

using DagnysBageriApi.Data;
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
            var materials = await _context.RawMaterials.ToListAsync();
            return Ok(new { success = true, data = materials});
        }
    }
}