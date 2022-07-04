using AutoMapper;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject;

namespace Fusap.TimeSheet.Application.UseCases.Users.V1.Get
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShowAllUsersUnderAdminRequest, ShowAllUsersUnderAdminDto>();

            CreateMap<ShowAllUsersRequest, ShowAllUsersDto>()
                .ForMember(x => x.RoleId, opt => opt.Ignore());

            CreateMap<ShowAllAdminsRequest, ShowAllUsersDto>()
                .ForMember(x => x.RoleId, opt => opt.Ignore());

            CreateMap<GetUsersHoursNoCompleteRequest, UsersHoursNoCompleteDto>();
        }
    }
}
