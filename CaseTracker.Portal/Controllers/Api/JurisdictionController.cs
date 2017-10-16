using AutoMapper;
using CaseTracker.Core.Models;
using CaseTracker.Data;
using CaseTracker.Data.Repositories;
using CaseTracker.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CaseTracker.Portal.Controllers.Api
{
    [Route("api/[controller]")]
    public class JurisdictionController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private const int PAGE_SIZE = 20;

        public JurisdictionController(AppDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpGet("list")]
        public object List(JurisdictionSearchViewModel viewModel)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();
            if (viewModel == null) viewModel = new JurisdictionSearchViewModel();

            var totalCount = _unitOfWork.Jurisdictions.Count();

            var jurisdictions = _unitOfWork.Jurisdictions.GetAll()
                .WithNameLike(viewModel.Name)
                .WithAbbreviationLike(viewModel.Abbreviation);

            var filteredCount = jurisdictions.Count();
            var totalPages = Math.Ceiling((double)filteredCount / viewModel.PageSize ?? PAGE_SIZE);
            var startRow = viewModel.PageSize * (viewModel.Page - 1) ?? 0;

            viewModel.TotalCount = totalCount;
            viewModel.FilteredCount = filteredCount;
            viewModel.TotalPages = totalPages;
            viewModel.Results = Mapper.Map<List<JurisdictionViewModel>>(jurisdictions.WithPaging(startRow, viewModel.PageSize));

            stopwatch.Stop();
            viewModel.ElapsedTime = stopwatch.Elapsed;
            return Ok(viewModel);
        }

        [HttpPut()]
        public object Put([FromBody]JurisdictionViewModel model)
        {
            if (model == null) return BadRequest("No Jurisdiction to update");

            var jurisdiction = _unitOfWork.Jurisdictions.GetById(model.Id);
            if (jurisdiction == null) return NotFound("Jurisdiction not found");

            jurisdiction.Name = model.Name;
            jurisdiction.Abbreviation = model.Abbreviation;

            _unitOfWork.Complete();
            return Ok(jurisdiction);
        }

        [HttpPost()]
        public object Post([FromBody]JurisdictionViewModel model)
        {
            if (model == null) return BadRequest("No Jurisdiction to add");

            var jurisdiction = new Jurisdiction()
            {
                Name = model.Name,
                Abbreviation = model.Abbreviation
            };
            _unitOfWork.Jurisdictions.Add(jurisdiction);

            _unitOfWork.Complete();

            return Ok(jurisdiction);
        }

        [HttpDelete, Route("{id}")]
        public object Delete(int id)
        {
            var jurisdiction = _unitOfWork.Jurisdictions.GetById(id);

            if (jurisdiction == null) return BadRequest("Jurisdiction not found");

            if (_unitOfWork.Courts.GetAll().WithJurisdictionId(id).Any()) return BadRequest("Unable to delete. Jurisdiction has courts assigned.");

            _unitOfWork.Jurisdictions.Remove(jurisdiction);
            _unitOfWork.Complete();

            return Ok("Jurisdiction deleted");
        }
    }
}