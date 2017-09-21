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
            var list = await context.Courts.OrderByDescending(c => c.Id).Include("Jurisdiction").ToListAsync();
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
            court.JurisdictionId = model.JurisdictionId;

            await context.SaveChangesAsync();
            return Ok(court);
        }

        [HttpPost()]
        public async Task<object> Post([FromBody]CourtViewModel model)
        {
            if (model == null) return BadRequest("No court to add");

            var court = new Court()
            {
                Name = model.Name,
                Abbreviation = model.Abbreviation,
                JurisdictionId = model.JurisdictionId
            };
            await context.Courts.AddAsync(court);
            await context.SaveChangesAsync();

            return Ok(court);
        }

        [HttpDelete, Route("{id}")]
        public async Task<object> Delete(int id)
        {
            var court = await context.Courts.FindAsync(id);

            if (court == null) return BadRequest("Court not found");

            context.Courts.Remove(court);

            await context.SaveChangesAsync();

            return Ok("Court deleted");
        }
    }
}