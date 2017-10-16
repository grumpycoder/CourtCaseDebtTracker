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
    public class CourtController : Controller
    {
        private const int PAGE_SIZE = 20;
        private readonly UnitOfWork _unitOfWork;

        public CourtController(AppDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpGet("list")]
        public object List(CourtSearchViewModel viewModel)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();
            if (viewModel == null) viewModel = new CourtSearchViewModel();

            var totalCount = _unitOfWork.Courts.Count();

            var courts = _unitOfWork.Courts.GetAll()
                .WithNameLike(viewModel.Name)
                .WithJurisdictionLike(viewModel.Jurisdiction)
                .WithAbbreviationLike(viewModel.Abbreviation);

            var filteredCount = courts.Count();
            var totalPages = Math.Ceiling((double)filteredCount / viewModel.PageSize ?? PAGE_SIZE);
            var startRow = viewModel.PageSize * (viewModel.Page - 1) ?? 0;

            viewModel.TotalCount = totalCount;
            viewModel.FilteredCount = filteredCount;
            viewModel.TotalPages = totalPages;
            viewModel.Results = Mapper.Map<List<CourtViewModel>>(courts.WithPaging(startRow, viewModel.PageSize));

            stopwatch.Stop();
            viewModel.ElapsedTime = stopwatch.Elapsed;
            return Ok(viewModel);
        }

        [HttpPut()]
        public object Put([FromBody]CourtViewModel model)
        {
            if (model == null) return BadRequest("No court to update");

            var court = _unitOfWork.Courts.GetById(model.Id);

            if (court == null) return NotFound("Court not found");

            court.Name = model.Name;
            court.Abbreviation = model.Abbreviation;
            court.JurisdictionId = model.JurisdictionId;

            _unitOfWork.Complete();
            return Ok(court);
        }

        [HttpPost()]
        public object Post([FromBody]CourtViewModel model)
        {
            if (model == null) return BadRequest("No court to add");

            var court = new Court()
            {
                Name = model.Name,
                Abbreviation = model.Abbreviation,
                JurisdictionId = model.JurisdictionId
            };

            _unitOfWork.Courts.Add(court);

            _unitOfWork.Complete();

            return Ok(court);
        }

        [HttpDelete, Route("{id}")]
        public object Delete(int id)
        {

            var court = _unitOfWork.Courts.GetByIdWithDetails(id);
            if (court == null) return BadRequest("Court not found");

            if (court.Filings.Any()) return BadRequest("Unable to delete. Court has cases assigned.");

            _unitOfWork.Courts.Remove(court);

            _unitOfWork.Complete();

            return Ok("Court deleted");
        }
    }
}