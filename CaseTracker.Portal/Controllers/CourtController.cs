using System.Linq;
using System.Threading.Tasks;
using CaseTracker.Core.Models;
using CaseTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaseTracker.Portal.ViewModels;
using System;
using CaseTracker.Portal.Helpers;
using System.Diagnostics;
using AutoMapper;
using System.Collections.Generic;

namespace CaseTracker.Portal.Controllers
{
    [Route("api/[controller]")]
    public class CourtController : Controller
    {
        private readonly AppDbContext context;
        private const int PAGE_SIZE = 20;

        public CourtController(AppDbContext _context)
        {
            context = _context;
        }

        [HttpGet("list")]
        public async Task<object> List(CourtSearchViewModel pager)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();
            if (pager == null) pager = new CourtSearchViewModel();

            var query = context.Courts;
            var totalCount = await query.CountAsync();

            var pred = PredicateBuilder.True<Court>();
            if (!string.IsNullOrWhiteSpace(pager.Name)) pred = pred.And(p => p.Name.Contains(pager.Name));
            if (!string.IsNullOrWhiteSpace(pager.Jurisdiction)) pred = pred.And(p => p.Jurisdiction.Name.Contains(pager.Jurisdiction));
            if (!string.IsNullOrWhiteSpace(pager.Abbreviation)) pred = pred.And(p => p.Abbreviation.Contains(pager.Abbreviation));

            var filteredQuery = query.Where(pred);
            var pagerCount = filteredQuery.Count();
            var totalPages = Math.Ceiling((double)pagerCount / pager.PageSize ?? PAGE_SIZE);

            var results = await filteredQuery.Include(f => f.Jurisdiction).Include(f => f.Filings).Where(pred)
                .Order(pager.OrderBy, pager.OrderDirection == "desc" ? SortDirection.Descending : SortDirection.Ascending)
                .Skip(pager.PageSize * (pager.Page - 1) ?? 0)
                .Take(pager.PageSize ?? PAGE_SIZE)
                .ToListAsync();

            pager.TotalCount = totalCount;
            pager.FilteredCount = pagerCount;
            pager.TotalPages = totalPages;
            pager.Results = Mapper.Map<List<CourtViewModel>>(results);
            stopwatch.Stop();
            pager.ElapsedTime = stopwatch.Elapsed;
            return Ok(pager);
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
            var court = await context.Courts.Include("Filings").FirstAsync(c => c.Id == id);

            if (court == null) return BadRequest("Court not found");
            if (court.Filings.Count() > 0) return BadRequest("Unable to delete. Court has cases assigned.");

            context.Courts.Remove(court);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong. Contact support.");
            }


            return Ok("Court deleted");
        }
    }
}