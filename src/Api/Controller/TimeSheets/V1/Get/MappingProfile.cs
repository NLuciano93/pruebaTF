using AutoMapper;
using Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Get;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;

namespace Fusap.TimeSheet.Api.Controller.TimeSheets.V1
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShowAllTimeSheetsInput, ShowAllTimeSheetsRequest>()
                .ForMember(x => x.SortDirection, opt => opt.MapFrom(
                    src => !string.IsNullOrEmpty(src.SortDirection) ? src.SortDirection.ToUpper() : null)
                );
        }
    }
}
