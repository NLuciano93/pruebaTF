using Fusap.Common.Mediator;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class CheckProjectNameExistsRequest : Request
    {
        public string ProjectName { get; set; }

        public CheckProjectNameExistsRequest(string projectName)
        {
            ProjectName = projectName;
        }
    }
}
