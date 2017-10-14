using AutoMapper;
using CaseTracker.Core.Models;
using CaseTracker.Data;
using CaseTracker.Portal.Persistence;
using CaseTracker.Portal.Repositories;
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
        private const int PAGE_SIZE = 20;

        public CaseController(AppDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpGet("list")]
        public async Task<object> List(CaseSearchViewModel viewModel)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();
            if (viewModel == null) viewModel = new CaseSearchViewModel();

            var totalCount = _unitOfWork.Cases.Count();

            var cases = _unitOfWork.Cases.GetAll()
                .WithCaptionLike(viewModel.Caption)
                .WithCaseNumberLike(viewModel.CaseNumber)
                .WithJudgeLike(viewModel.Judge)
                .WithCourtNameLike(viewModel.CourtName);

            var filteredCount = cases.Count();
            var totalPages = Math.Ceiling((double)filteredCount / viewModel.PageSize ?? PAGE_SIZE);
            var startRow = viewModel.PageSize * (viewModel.Page - 1) ?? 0;

            viewModel.TotalCount = totalCount;
            viewModel.FilteredCount = filteredCount;
            viewModel.TotalPages = totalPages;
            viewModel.Results = Mapper.Map<List<FilingViewModel>>(cases.WithPaging(startRow, viewModel.PageSize));

            stopwatch.Stop();
            viewModel.ElapsedTime = stopwatch.Elapsed;
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            var @case = _unitOfWork.Cases.GetByIdWithDetails(id);
            var model = Mapper.Map<FilingViewModel>(@case);
            //TODO: Refactor into model
            model.CanDelete = User.HasClaim("LoginProvider", "SPLC");
            return Ok(model);
        }

        [HttpPut()]
        public async Task<object> Put([FromBody]CaseViewModel model)
        {
            if (model == null) return BadRequest("No case to update");

            var @case = _unitOfWork.Cases.GetByIdWithDetails(model.Id);
            if (@case == null) return NotFound("Case not found");

            //TODO: Using Automapper
            @case.Caption = model.Caption;
            @case.Judge = model.Judge;
            @case.Summary = model.Summary;
            @case.CaseNumber = model.CaseNumber;
            @case.DateFiled = model.DateFiled;
            @case.CourtId = model.CourtId;

            _unitOfWork.Complete();

            //TODO: Something smells
            @case = _unitOfWork.Cases.GetByIdWithDetails(model.Id);
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

            _unitOfWork.Cases.Add(@case);
            _unitOfWork.Complete();

            var m = _unitOfWork.Cases.GetByIdWithDetails(@case.Id);
            var c = Mapper.Map<FilingViewModel>(m);
            return Ok(c);
        }

        [HttpDelete(), Route("{id}")]
        public async Task<object> Delete(int id)
        {
            var @case = _unitOfWork.Cases.GetById(id);
            if (@case == null) return NotFound("Case not found");

            _unitOfWork.Cases.Remove(@case);
            _unitOfWork.Complete();

            return Ok("Deleted case");
        }

        [HttpPost("{caseId}/litigant")]
        public async Task<object> AddLitigant(int caseId, [FromBody]LitigantViewModel model)
        {
            if (model == null) return BadRequest("No litigant to add");

            var @case = _unitOfWork.Cases.GetById(caseId);
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
                    _unitOfWork.Litigants.AddDefendant(defendant);
                    _unitOfWork.Complete();
                    return Ok(defendant);
                    break;
                case LitigantType.Plaintiff:
                    plaintiff = new Plaintiff()
                    {
                        Name = model.Name,
                        FilingId = caseId
                    };
                    _unitOfWork.Litigants.AddPlaintiff(plaintiff);
                    _unitOfWork.Complete();

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

            var litigant = _unitOfWork.Litigants.GetById(id);
            if (litigant == null) return NotFound("Litigant not found");

            _unitOfWork.Litigants.Remove(litigant);
            _unitOfWork.Complete();

            return Ok("Delete litigant");
        }
    }
}