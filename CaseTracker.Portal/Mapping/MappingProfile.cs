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

            CreateMap<Case, CaseDetailViewModel>()
                .ForMember(d => d.Jurisdiction, opt => opt.MapFrom(s => s.Court.Jurisdiction.Name))
                .ForMember(d => d.CourtName, opt => opt.MapFrom(s => s.Court.Name))
                .ForMember(d => d.Tags, opt => opt.MapFrom(s => s.Tags.Select(t => t.Tag.Name)))
                .ReverseMap();


            CreateMap<Court, CourtViewModel>()
                .ForMember(d => d.Jurisdiction, opt => opt.MapFrom(s => s.Jurisdiction.Name))
                .ForMember(d => d.NumberCases, opt => opt.MapFrom(s => s.Filings.Count()))
                .ReverseMap();

            CreateMap<Jurisdiction, JurisdictionViewModel>().ReverseMap();

            CreateMap<Case, CaseViewModel>()
                .ForMember(d => d.Jurisdiction, opt => opt.MapFrom(s => s.Court.Jurisdiction.Name))
                .ForMember(d => d.Court, opt => opt.MapFrom(s => s.Court.Name))
                .ForMember(d => d.CourtId, opt => opt.MapFrom(s => s.CourtId))
                .ForMember(d => d.CaseNumber, opt => opt.MapFrom(s => s.CaseNumber))
                .ReverseMap();
        }
    }
}