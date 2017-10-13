using System.Threading.Tasks;
using CaseTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaseTracker.Portal.Controllers.Api
{
    public class TagController : Controller
    {
        private readonly AppDbContext context;

        public TagController(AppDbContext _context)
        {
            context = _context;
        }

        [HttpGet("list")]
        public async Task<object> List()
        {
            var tags = await context.Tags.ToListAsync();

            return Ok(tags);
        }
    }
}