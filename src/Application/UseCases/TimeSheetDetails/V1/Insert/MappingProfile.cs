using AutoMapper;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheetDetails.V1.Insert
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddTimeSheetDetailRequest, Domain.TimeSheetAggregate.Entities.TimeSheetDetails>()
                .ForMember(x => x.TimeSheetId, opt => opt.Ignore())
                .ForMember(x => x.CreatedOn, opt => opt.Ignore());
        }
    }
}
