using System.Linq;
using System.Threading.Tasks;
using CaseTracker.Data;
using CaseTracker.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaseTracker.Portal.Controllers
{
    [Route("api/[controller]")]
    public class CaseController : Controller
    {
        private readonly AppDbContext context;
        public CaseController(AppDbContext _context)
        {
            context = _context;
        }

        [HttpGet("list")]
        public async Task<object> List()
        {
            var list = await context.Filings.OrderBy(f => f.Id).Skip(0).Take(10).ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            var c = await context.Filings.Include("Defendants").Include("Plaintiffs").FirstOrDefaultAsync(f => f.Id == id);
            return Ok(c);
        }

        [HttpPut()]
        public async Task<object> Put([FromBody]CaseViewModel model)
        {
            if (model == null) return BadRequest("No case to update");

            var @case = await context.Filings.FindAsync(model.Id);
            if (@case == null) return NotFound("Case not found");

            @case.Caption = model.Caption;
            @case.Judge = model.Judge;
            @case.Summary = model.Summary;
            @case.CaseNumber = model.CaseNumber;

            await context.SaveChangesAsync();
            return Ok(@case);
        }

    }
}