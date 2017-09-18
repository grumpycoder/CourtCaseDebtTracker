using AutoMapper;
using CaseTracker.Core.Models;
using CaseTracker.Portal.ViewModels;
using System.Linq;

namespace CaseTracker.Portal.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<CreateEditFilingViewModel, Filing>().ReverseMap();
            // CreateMap<CreateEditCourtViewModel, Court>().ReverseMap();
            // CreateMap<CreateEditJurisdictionViewModel, Jurisdiction>().ReverseMap();
            // CreateMap<CreateEditCommentViewModel, Comment>().ReverseMap();
            //CreateMap<FilingViewModel, Filing>();
            CreateMap<Filing, FilingViewModel>()
                .ForMember(d => d.Jurisdiction, opt => opt.MapFrom(s => s.Court.Jurisdiction.Name))
                .ForMember(d => d.CourtName, opt => opt.MapFrom(s => s.Court.Name))
                .ReverseMap();
            // .ForMember(d => d.Tags, opt => opt.MapFrom(s => s.Tags.Select(t => t.Tag.Name)))
            // .ForMember(d => d.Plaintiffs, opt => opt.MapFrom(s => s.Plaintiffs.Select(t => t.Name)))
            // .ForMember(d => d.Defendants, opt => opt.MapFrom(s => s.Defendants.Select(t => t.Name)))
            ;
            // CreateMap<Court, CourtViewModel>()
            //     .ForMember(d => d.Jurisdiction, opt => opt.MapFrom(s => s.Jurisdiction.Name))
            //     .ForMember(d => d.NumberCases, opt => opt.MapFrom(s => s.Filings.Count()))
            //     .ReverseMap();
        }
    }
}