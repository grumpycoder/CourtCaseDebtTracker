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
using System.Threading.Tasks;

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
        public async Task<object> List(CaseSearchViewModel viewModel)
        {
            var stopwatch = Stopwatch.StartNew();

            if (viewModel == null) viewModel = new CaseSearchViewModel();

            var totalCount = await _unitOfWork.Cases.CountAsync();

            var cases = _unitOfWork.Cases.GetAll()
                    .WithCaptionLike(viewModel.Caption)
                    .WithCaseNumberLike(viewModel.CaseNumber)
                    .WithJudgeLike(viewModel.Judge)
                    .WithJurisdictionLike(viewModel.Jurisdiction)
                    .WithCourtNameLike(viewModel.Court)
                    .OrderBy(c => c.Id);

            var filteredCount = cases.Count();
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
        public async Task<object> Get(int id)
        {
            var @case = await _unitOfWork.Cases.GetByIdWithDetailsAsync(id);
            var model = Mapper.Map<CaseDetailViewModel>(@case);
            //TODO: Refactor into model
            model.CanDelete = User.HasClaim("LoginProvider", "SPLC");
            return Ok(model);
        }

        [HttpPut]
        public async Task<object> Put([FromBody]CaseFormViewModel model)
        {
            if (model == null) return BadRequest("No case to update");

            var @case = await _unitOfWork.Cases.GetByIdWithDetailsAsync(model.Id);
            if (@case == null) return NotFound("Case not found");

            //TODO: Use AutoMapper
            @case.CaseNumber = model.CaseNumber;
            @case.Caption = model.Caption;
            @case.Judge = model.Judge;
            @case.Summary = model.Summary;
            @case.CourtId = model.CourtId;
            @case.DateFiled = model.DateFiled;

            await _unitOfWork.CompleteAsync();

            @case = await _unitOfWork.Cases.GetByIdWithDetailsAsync(model.Id);

            return Ok(Mapper.Map<CaseDetailViewModel>(@case));
        }

        [HttpPost()]
        public async Task<object> Post([FromBody]CaseFormViewModel model)
        {
            if (model == null) return BadRequest("No case to update");

            //TODO: Use AutoMapper
            var @case = new Case()
            {
                Caption = model.Caption,
                Judge = model.Judge,
                Summary = model.Summary,
                CaseNumber = model.CaseNumber,
                CourtId = model.CourtId
            };

            _unitOfWork.Cases.Add(@case);
            await _unitOfWork.CompleteAsync();

            @case = await _unitOfWork.Cases.GetByIdWithDetailsAsync(@case.Id);
            return Ok(Mapper.Map<CaseDetailViewModel>(@case));
        }

        [HttpDelete(), Route("{id}")]
        public async Task<object> Delete(int id)
        {
            var @case = await _unitOfWork.Cases.GetByIdAsync(id);
            if (@case == null) return NotFound("Case not found");

            _unitOfWork.Cases.Remove(@case);
            await _unitOfWork.CompleteAsync();

            return Ok("Deleted case");
        }

        [HttpPost("{caseId}/litigant")]
        public async Task<object> AddLitigant(int caseId, [FromBody]LitigantViewModel model)
        {
            if (model == null) return BadRequest("No litigant to add");

            var @case = await _unitOfWork.Cases.GetByIdAsync(caseId);
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
                    await _unitOfWork.CompleteAsync();
                    return Ok(defendant);
                case LitigantType.Plaintiff:
                    var plaintiff = new Plaintiff()
                    {
                        Name = model.Name,
                        FilingId = caseId
                    };
                    _unitOfWork.Litigants.AddPlaintiff(plaintiff);
                    await _unitOfWork.CompleteAsync();

                    return Ok(plaintiff);
                default:
                    return Ok();
            };
        }

        [HttpDelete("{caseId}/litigant/{id}")]
        public async Task<object> DeleteLitigant(int caseId, int id)
        {
            var litigant = await _unitOfWork.Litigants.GetByIdAsync(id);
            if (litigant == null) return NotFound("Litigant not found");

            _unitOfWork.Litigants.Remove(litigant);
            await _unitOfWork.CompleteAsync();

            return Ok("Delete litigant");
        }
    }
}