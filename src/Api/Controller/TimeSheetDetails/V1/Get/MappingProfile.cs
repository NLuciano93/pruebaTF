using AutoMapper;
using Fusap.TimeSheet.Application.UseCases.TimeSheetDetails.V1.Get;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;

namespace Fusap.TimeSheet.Api.Controller.TimeSheetDetails.V1
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetUserDateIsUsedInput, GetUserDateIsUsedRequest>();
        }
    }
}
