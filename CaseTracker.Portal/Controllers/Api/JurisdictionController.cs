using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CaseTracker.Core.Models;
using CaseTracker.Data;
using CaseTracker.Portal.Helpers;
using CaseTracker.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaseTracker.Portal.Controllers.Api
{
    [Route("api/[controller]")]
    public class JurisdictionController : Controller
    {
        private readonly AppDbContext context;
        private const int PAGE_SIZE = 20;

        public JurisdictionController(AppDbContext _context)
        {
            context = _context;
        }

        [HttpGet("list")]
        public async Task<object> List(JurisdictionSearchViewModel pager)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();
            if (pager == null) pager = new JurisdictionSearchViewModel();

            var query = context.Jurisdictions;
            var totalCount = await query.CountAsync();

            var pred = PredicateBuilder.True<Jurisdiction>();
            if (!string.IsNullOrWhiteSpace(pager.Name)) pred = pred.And(p => p.Name.Contains(pager.Name));
            if (!string.IsNullOrWhiteSpace(pager.Abbreviation)) pred = pred.And(p => p.Abbreviation.Contains(pager.Abbreviation));

            var filteredQuery = query.Where(pred);
            var pagerCount = filteredQuery.Count();
            var totalPages = Math.Ceiling((double)pagerCount / pager.PageSize ?? PAGE_SIZE);

            var results = await filteredQuery.Where(pred)
                .Order(pager.OrderBy, pager.OrderDirection == "desc" ? SortDirection.Descending : SortDirection.Ascending)
                .Skip(pager.PageSize * (pager.Page - 1) ?? 0)
                .Take(pager.PageSize ?? PAGE_SIZE)
                .ToListAsync();

            pager.TotalCount = totalCount;
            pager.FilteredCount = pagerCount;
            pager.TotalPages = totalPages;
            pager.Results = Mapper.Map<List<JurisdictionViewModel>>(results);
            stopwatch.Stop();
            pager.ElapsedTime = stopwatch.Elapsed;
            return Ok(pager);
        }

        [HttpPut()]
        public async Task<object> Put([FromBody]JurisdictionViewModel model)
        {
            if (model == null) return BadRequest("No Jurisdiction to update");

            var jurisdiction = await context.Jurisdictions.FindAsync(model.Id);
            if (jurisdiction == null) return NotFound("Jurisdiction not found");

            jurisdiction.Name = model.Name;
            jurisdiction.Abbreviation = model.Abbreviation;

            await context.SaveChangesAsync();
            return Ok(jurisdiction);
        }

        [HttpPost()]
        public async Task<object> Post([FromBody]JurisdictionViewModel model)
        {
            if (model == null) return BadRequest("No Jurisdiction to add");

            var jurisdiction = new Jurisdiction()
            {
                Name = model.Name,
                Abbreviation = model.Abbreviation
            };
            await context.Jurisdictions.AddAsync(jurisdiction);
            await context.SaveChangesAsync();

            return Ok(jurisdiction);
        }

        [HttpDelete, Route("{id}")]
        public async Task<object> Delete(int id)
        {
            var jurisdiction = await context.Jurisdictions.FirstAsync(c => c.Id == id);

            if (jurisdiction == null) return BadRequest("Jurisdiction not found");
            if (context.Courts.Count(c => c.JurisdictionId == id) > 0) return BadRequest("Unable to delete. Jurisdiction has courts assigned.");

            context.Jurisdictions.Remove(jurisdiction);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong. Contact support.");
            }

            return Ok("Jurisdiction deleted");
        }
    }
}