using AutoMapper;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Entities;

namespace Fusap.TimeSheet.Application.UseCases.Registrations.V1.Insert
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddRegistrationRequest, Registration>()
                .ForMember(x => x.RegistrationId, opt => opt.Ignore())
                .ForMember(x => x.RoleId, opt => opt.Ignore())
                .ForMember(x => x.CreatedOn, opt => opt.Ignore())
                .ForMember(x => x.DateofJoining, opt => opt.Ignore())
                .ForMember(x => x.ForceChangePassword, opt => opt.Ignore())
                .ForMember(x => x.MevetoLogin, opt => opt.Ignore())
                .ForMember(x => x.EmployeeId, opt => opt.Ignore());
        }
    }
}
