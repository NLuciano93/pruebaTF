using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class GetProjectMasterByIdRequest : Request<ProjectMasterResponse>
    {
        public int ProjectId { get; set; }

        public GetProjectMasterByIdRequest(int projectId)
        {
            ProjectId = projectId;
        }
    }
}
