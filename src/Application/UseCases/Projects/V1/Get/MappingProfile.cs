using AutoMapper;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShowAllProjectsRequest, ShowAllProjectsDto>();
        }
    }
}
