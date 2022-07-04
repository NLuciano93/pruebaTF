using AutoMapper;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Entities;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Insert
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AssignProjectToUsersInput, AssignProjectToUsersDto>()
                .ForMember(x => x.ProjectId, opt => opt.Ignore());

            CreateMap<AssignProjectToUsersInput.UserProjectModel, AssignProjectToUsersDto.UserProjectModel>();

            CreateMap<AddProjectMasterRequest, ProjectMaster>()
                .ForMember(x => x.ProjectId, opt => opt.Ignore());
        }
    }
}
