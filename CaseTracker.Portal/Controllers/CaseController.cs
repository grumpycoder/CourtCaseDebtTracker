using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CaseTracker.Core.Models;
using CaseTracker.Data;
using CaseTracker.Portal.Helpers;
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
        private const int PAGE_SIZE = 20;

        public CaseController(AppDbContext _context)
        {
            context = _context;
        }

        [HttpGet("list")]
        public async Task<object> List(CaseSearchViewModel pager)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();
            if (pager == null) pager = new CaseSearchViewModel();

            var query = context.Filings;
            var totalCount = await query.CountAsync();

            var pred = PredicateBuilder.True<Filing>();
            if (!string.IsNullOrWhiteSpace(pager.Caption)) pred = pred.And(p => p.Caption.Contains(pager.Caption));
            if (!string.IsNullOrWhiteSpace(pager.CaseNumber)) pred = pred.And(p => p.CaseNumber.Contains(pager.CaseNumber));
            if (!string.IsNullOrWhiteSpace(pager.CourtName)) pred = pred.And(p => p.Court.Name.Contains(pager.CourtName));
            if (!string.IsNullOrWhiteSpace(pager.Judge)) pred = pred.And(p => p.Judge.StartsWith(pager.Judge));

            var filteredQuery = query.Where(pred);
            var pagerCount = filteredQuery.Count();
            var totalPages = Math.Ceiling((double)pagerCount / pager.PageSize ?? PAGE_SIZE);

            var results = await filteredQuery.Include(f => f.Court.Jurisdiction).Where(pred)
                .Order(pager.OrderBy, pager.OrderDirection == "desc" ? SortDirection.Descending : SortDirection.Ascending)
                .Skip(pager.PageSize * (pager.Page - 1) ?? 0)
                .Take(pager.PageSize ?? PAGE_SIZE)
                .ToListAsync();

            pager.TotalCount = totalCount;
            pager.FilteredCount = pagerCount;
            pager.TotalPages = totalPages;
            pager.Results = Mapper.Map<List<FilingViewModel>>(results);
            stopwatch.Stop();
            pager.ElapsedTime = stopwatch.Elapsed;
            return Ok(pager);

            // var list = await context.Filings.Include(f => f.Court.Jurisdiction).OrderByDescending(f => f.Id).Skip(0).Take(10).ToListAsync();

            // var model = Mapper.Map<List<FilingViewModel>>(list);

            // return Ok(model);
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

            var @case = await context.Filings.Include("Court").FirstOrDefaultAsync(f => f.Id == model.Id);
            if (@case == null) return NotFound("Case not found");

            //TODO: Using Automapper
            @case.Caption = model.Caption;
            @case.Judge = model.Judge;
            @case.Summary = model.Summary;
            @case.CaseNumber = model.CaseNumber;
            @case.DateFiled = model.DateFiled;
            @case.CourtId = model.CourtId;

            await context.SaveChangesAsync();
            //TODO: Something smells
            @case = await context.Filings.Include(f => f.Court.Jurisdiction).Include("Defendants").Include("Plaintiffs").FirstOrDefaultAsync(f => f.Id == model.Id);
            var c = Mapper.Map<FilingViewModel>(@case);
            return Ok(c);
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

            var m = await context.Filings.Include(f => f.Court.Jurisdiction).FirstAsync(f => f.Id == @case.Id);
            var c = Mapper.Map<FilingViewModel>(m);
            return Ok(c);
        }

        [HttpDelete(), Route("{id}")]
        public async Task<object> Delete(int id)
        {
            var @case = await context.Filings.FindAsync(id);
            if (@case == null) return NotFound("Case not found");

            context.Filings.Remove(@case);
            await context.SaveChangesAsync();

            return Ok("Deleted case");
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