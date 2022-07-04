using AutoMapper;

namespace Fusap.TimeSheet.Application.UseCases.DescriptionTb.V1.Insert
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddDescriptionTbRequest, Domain.TimeSheetAggregate.Entities.DescriptionTb>()
                .ForMember(x => x.DescriptionId, opt => opt.Ignore())
                .ForMember(x => x.CreatedOn, opt => opt.Ignore());
        }
    }
}
