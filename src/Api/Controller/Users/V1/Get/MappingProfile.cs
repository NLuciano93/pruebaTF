using AutoMapper;
using Fusap.TimeSheet.Application.UseCases.Users.V1.Get;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;

namespace Fusap.TimeSheet.Api.Controller.Users.V1.Get
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SearchAllUsersUnderAdminsInput, ShowAllUsersUnderAdminRequest>()
                .ForMember(x => x.AdminUserId, opt => opt.Ignore())
                .ForMember(x => x.SortDirection, opt => opt.MapFrom(
                    src => !string.IsNullOrEmpty(src.SortDirection) ? src.SortDirection.ToUpper() : null)
                );

            CreateMap<ShowAllUsersInput, ShowAllUsersRequest>()
                .ForMember(x => x.SortDirection, opt => opt.MapFrom(
                    src => !string.IsNullOrEmpty(src.SortDirection) ? src.SortDirection.ToUpper() : null)
                );

            CreateMap<ShowAllAdminsInput, ShowAllAdminsRequest>()
                .ForMember(x => x.SortDirection, opt => opt.MapFrom(
                    src => !string.IsNullOrEmpty(src.SortDirection) ? src.SortDirection.ToUpper() : null)
                );

            CreateMap<GetUsersHoursNoCompleteInput, GetUsersHoursNoCompleteRequest>()
                .ForMember(x => x.SortDirection, opt => opt.MapFrom(
                    src => !string.IsNullOrEmpty(src.SortDirection) ? src.SortDirection.ToUpper() : null)
                );
        }
    }
}
