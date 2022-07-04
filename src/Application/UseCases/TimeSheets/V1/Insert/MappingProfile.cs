using AutoMapper;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Entities;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Insert
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddTimeSheetMasterRequest, TimeSheetMaster>()
                .ForMember(x => x.TimeSheetMasterId, opt => opt.Ignore())
                .ForMember(x => x.CreatedOn, opt => opt.Ignore());
        }
    }
}
