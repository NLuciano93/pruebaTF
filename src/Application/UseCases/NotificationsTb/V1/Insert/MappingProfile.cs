using AutoMapper;

namespace Fusap.TimeSheet.Application.UseCases.NotificationsTb.V1.Insert
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddNotificationsTbRequest, Domain.TimeSheetAggregate.Entities.NotificationsTb>()
                .ForMember(x => x.NotificationsId, opt => opt.Ignore())
                .ForMember(x => x.CreatedOn, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore());
        }
    }
}
