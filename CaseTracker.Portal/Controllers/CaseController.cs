using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CaseTracker.Core.Models;
using CaseTracker.Data;
using CaseTracker.Portal.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
            var list = await context.Filings.Include(f => f.Court.Jurisdiction).OrderBy(f => f.Id).Skip(0).Take(10).ToListAsync();

            var model = Mapper.Map<List<FilingViewModel>>(list);

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            var @case = await context.Filings.Include(f => f.Court.Jurisdiction).Include("Defendants").Include("Plaintiffs").FirstOrDefaultAsync(f => f.Id == id);
            var model = Mapper.Map<FilingViewModel>(@case);
            model.CanDelete = User.HasClaim("LoginProvider", "SPLC");
            return Ok(model);
        }

        [HttpPut()]
        public async Task<object> Put([FromBody]CaseViewModel model)
        {
            if (model == null) return BadRequest("No case to update");
            model.DateFiled = new DateTime();

            var @case = await context.Filings.FindAsync(model.Id);
            if (@case == null) return NotFound("Case not found");

            @case.Caption = model.Caption;
            @case.Judge = model.Judge;
            @case.Summary = model.Summary;
            @case.CaseNumber = model.CaseNumber;
            @case.DateFiled = model.DateFiled;

            await context.SaveChangesAsync();
            return Ok(@case);
        }

        [HttpPost()]
        public async Task<object> Post([FromBody]CaseViewModel model)
        {
            if (model == null) return BadRequest("No case to update");

            var @case = new Filing()
            {
                Caption = model.Caption,
                Judge = model.Judge,
                Summary = model.Summary,
                CaseNumber = model.CaseNumber,
                CourtId = model.CourtId
            };
            await context.Filings.AddAsync(@case);
            await context.SaveChangesAsync();

            return Ok(@case);
        }

        [HttpPost("{caseId}/litigant")]
        public async Task<object> AddLitigant(int caseId, [FromBody]LitigantViewModel model)
        {
            if (model == null) return BadRequest("No litigant to add");

            var @case = await context.Filings.FindAsync(caseId);
            if (@case == null) return NotFound("Case not found");

            Defendant defendant;
            Plaintiff plaintiff;

            switch (model.Type)
            {
                case LitigantType.Defendant:
                    defendant = new Defendant()
                    {
                        Name = model.Name,
                        FilingId = caseId
                    };
                    context.Defendants.Add(defendant);
                    await context.SaveChangesAsync();
                    return Ok(defendant);
                    break;
                case LitigantType.Plaintiff:
                    plaintiff = new Plaintiff()
                    {
                        Name = model.Name,
                        FilingId = caseId
                    };
                    context.Plaintiffs.Add(plaintiff);
                    await context.SaveChangesAsync();
                    return Ok(plaintiff);
                    break;
                default:
                    return Ok();
                    break;
            };

            //return Ok();
        }

        [HttpDelete("{caseId}/litigant/{id}")]
        public async Task<object> DeleteLitigant(int caseId, int id)
        {
            var litigant = await context.Litigants.FindAsync(id);
            if (litigant == null) return NotFound("Litigant not found");

            context.Litigants.Remove(litigant);

            await context.SaveChangesAsync();

            return Ok("Delete litigant");
        }
    }
}