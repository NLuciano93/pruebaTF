using Fusap.Common.Mediator;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Insert
{
    public class AssignProjectToUsersRequest : Request
    {
        public int ProjectId { get; set; }
        public AssignProjectToUsersInput AssignProjectToUsersInput { get; set; }

        public AssignProjectToUsersRequest(int projectId, AssignProjectToUsersInput assignProjectToUsersInput)
        {
            ProjectId = projectId;
            AssignProjectToUsersInput = assignProjectToUsersInput;
        }
    }
}
