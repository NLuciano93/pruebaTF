using Fusap.Common.Mediator;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Delete
{
    public class DeleteProjectMasterRequest : Request
    {
        public DeleteProjectMasterRequest(int projectId)
        {
            ProjectId = projectId;
        }

        public int ProjectId { get; set; }
    }
}
