
using DagnysBageriApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace DagnysBageriApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        // [HttpGet()]
    }
}