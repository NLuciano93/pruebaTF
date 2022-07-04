using AutoMapper;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheetAuditTb.Insert
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InsertTimeSheetAuditLogRequest, Domain.TimeSheetAggregate.Entities.TimeSheetAuditTb>()
               .ForMember(x => x.ApprovalTimeSheetLogId, opt => opt.Ignore())
               .ForMember(x => x.ProcessedDate, opt => opt.Ignore())
               .ForMember(x => x.CreatedOn, opt => opt.Ignore());
        }
    }
}
