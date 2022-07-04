using AutoMapper;
using Fusap.TimeSheet.Application.UseCases.Projects.V1.Get;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;

namespace Fusap.TimeSheet.Api.Controller.Projects.V1.Get
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShowAllProjectsInput, ShowAllProjectsRequest>()
                .ForMember(x => x.SortDirection, opt => opt.MapFrom(
                    src => !string.IsNullOrEmpty(src.SortDirection) ? src.SortDirection.ToUpper() : null)
                );
        }
    }
}
