using Fusap.Common.Mediator;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class GetProjectNameByIdRequest : Request<string>
    {
        public int ProjectId { get; set; }

        public GetProjectNameByIdRequest(int projectId)
        {
            ProjectId = projectId;
        }
    }
}
