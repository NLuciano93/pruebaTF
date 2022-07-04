using AutoMapper;
using Fusap.TimeSheet.Application.UseCases.NotificationsTb.V1.Get;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;

namespace Fusap.TimeSheet.Api.Controller.NotificationsTb.V1.Get
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShowNotificationsTbInput, ShowNotificationsTbRequest>()
                .ForMember(x => x.SortDirection, opt => opt.MapFrom(
                    src => !string.IsNullOrEmpty(src.SortDirection) ? src.SortDirection.ToUpper() : null)
                );
        }
    }
}
