using System.Linq;
using System.Threading.Tasks;
using CaseTracker.Core.Models;
using CaseTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaseTracker.Portal.ViewModels;

namespace CaseTracker.Portal.Controllers
{
    [Route("api/[controller]")]
    public class CourtController : Controller
    {
        private readonly AppDbContext context;
        public CourtController(AppDbContext _context)
        {
            context = _context;
        }

        [HttpGet("list")]
        public async Task<object> List()
        {
            var list = await context.Courts.ToListAsync();
            return Ok(list);
        }

        [HttpPut()]
        public async Task<object> Put([FromBody]CourtViewModel model)
        {
            if (model == null) return BadRequest("No court to update");

            var court = await context.Courts.FindAsync(model.Id);
            if (court == null) return NotFound("Court not found");

            court.Name = model.Name;
            court.Abbreviation = model.Abbreviation;

            await context.SaveChangesAsync();
            return Ok(court);
        }
    }
}