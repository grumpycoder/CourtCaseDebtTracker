using System.Threading.Tasks;
using CaseTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaseTracker.Portal.Controllers
{
    [Route("api/[controller]")]
    public class JurisdictionController : Controller
    {
        private readonly AppDbContext context;
        public JurisdictionController(AppDbContext _context)
        {
            context = _context;
        }

        [HttpGet("list")]
        public async Task<object> List()
        {
            var list = await context.Courts.ToListAsync();
            return Ok(list);
        }
    }
}