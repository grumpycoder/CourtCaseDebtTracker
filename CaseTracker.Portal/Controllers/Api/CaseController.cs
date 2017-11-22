using AutoMapper;
using CaseTracker.Core.Models;
using CaseTracker.Data;
using CaseTracker.Data.Repositories;
using CaseTracker.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CaseTracker.Portal.Controllers.Api
{
    [Route("api/[controller]")]
    public class CaseController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private const int PageSize = 20;

        public CaseController(AppDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpGet("list")]
        public object List(CaseSearchViewModel viewModel)
        {
            var stopwatch = Stopwatch.StartNew();

            if (viewModel == null) viewModel = new CaseSearchViewModel();

            var totalCount = _unitOfWork.Cases.Count();

            var cases = _unitOfWork.Cases.GetAll()
                .WithCaptionLike(viewModel.Caption)
                .WithJudgeLike(viewModel.Judge)
                .WithJurisdictionLike(viewModel.Jurisdiction)
                .WithCourtNameLike(viewModel.Court);

            var filteredCount = 10; //cases.Count();
            var totalPages = Math.Ceiling((double)filteredCount / viewModel.PageSize ?? PageSize);
            var startRow = viewModel.PageSize * (viewModel.Page - 1) ?? 0;

            viewModel.TotalCount = totalCount;
            viewModel.FilteredCount = filteredCount;
            viewModel.TotalPages = totalPages;
            viewModel.Results = Mapper.Map<List<CaseViewModel>>(cases.WithPaging(startRow, viewModel.PageSize));

            stopwatch.Stop();
            viewModel.ElapsedTime = stopwatch.Elapsed;
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public object Get(int id)
        {
            var @case = _unitOfWork.Cases.GetByIdWithDetails(id);
            var model = Mapper.Map<CaseDetailViewModel>(@case);
            //TODO: Refactor into model
            model.CanDelete = User.HasClaim("LoginProvider", "SPLC");
            return Ok(model);
        }

        [HttpPut]
        public object Put([FromBody]CaseFormViewModel model)
        {
            if (model == null) return BadRequest("No case to update");

            var @case = _unitOfWork.Cases.GetByIdWithDetails(model.Id);
            if (@case == null) return NotFound("Case not found");

            //TODO: Use AutoMapper
            @case.Caption = model.Caption;
            @case.Judge = model.Judge;
            @case.Summary = model.Summary;
            @case.CourtId = model.CourtId;
            @case.Date = model.Date;

            _unitOfWork.Complete();

            @case = _unitOfWork.Cases.GetByIdWithDetails(model.Id);

            return Ok(Mapper.Map<CaseViewModel>(@case));
        }

        [HttpPost()]
        public object Post([FromBody]CaseFormViewModel model)
        {
            if (model == null) return BadRequest("No case to update");

            //TODO: Use AutoMapper
            var @case = new Case()
            {
                Caption = model.Caption,
                Judge = model.Judge,
                Summary = model.Summary,
                CourtId = model.CourtId
            };

            _unitOfWork.Cases.Add(@case);
            _unitOfWork.Complete();

            @case = _unitOfWork.Cases.GetByIdWithDetails(@case.Id);
            return Ok(Mapper.Map<CaseViewModel>(@case));
        }

        [HttpDelete(), Route("{id}")]
        public object Delete(int id)
        {
            var @case = _unitOfWork.Cases.GetById(id);
            if (@case == null) return NotFound("Case not found");

            _unitOfWork.Cases.Remove(@case);
            _unitOfWork.Complete();

            return Ok("Deleted case");
        }

        [HttpPost("{caseId}/litigant")]
        public object AddLitigant(int caseId, [FromBody]LitigantViewModel model)
        {
            if (model == null) return BadRequest("No litigant to add");

            var @case = _unitOfWork.Cases.GetById(caseId);
            if (@case == null) return NotFound("Case not found");

            //TODO: Code smell
            switch (model.Type)
            {
                case LitigantType.Defendant:
                    var defendant = new Defendant()
                    {
                        Name = model.Name,
                        FilingId = caseId
                    };
                    _unitOfWork.Litigants.AddDefendant(defendant);
                    _unitOfWork.Complete();
                    return Ok(defendant);
                case LitigantType.Plaintiff:
                    var plaintiff = new Plaintiff()
                    {
                        Name = model.Name,
                        FilingId = caseId
                    };
                    _unitOfWork.Litigants.AddPlaintiff(plaintiff);
                    _unitOfWork.Complete();

                    return Ok(plaintiff);
                default:
                    return Ok();
            };
        }

        [HttpDelete("{caseId}/litigant/{id}")]
        public object DeleteLitigant(int caseId, int id)
        {
            var litigant = _unitOfWork.Litigants.GetById(id);
            if (litigant == null) return NotFound("Litigant not found");

            _unitOfWork.Litigants.Remove(litigant);
            _unitOfWork.Complete();

            return Ok("Delete litigant");
        }
    }
}