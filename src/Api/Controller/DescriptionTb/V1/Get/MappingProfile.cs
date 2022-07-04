using AutoMapper;
using Fusap.TimeSheet.Application.UseCases.DescriptionTb.V1.Get;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;

namespace Fusap.TimeSheet.Api.Controller.DescriptionTb.V1.Get
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetDescriptionTbInput, GetDescriptionTbRequest>();
        }
    }
}
