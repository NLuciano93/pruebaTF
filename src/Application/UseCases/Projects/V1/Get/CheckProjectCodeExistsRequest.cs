using Fusap.Common.Mediator;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class CheckProjectCodeExistsRequest : Request
    {
        public string ProjectCode { get; set; }

        public CheckProjectCodeExistsRequest(string projectCode)
        {
            ProjectCode = projectCode;
        }
    }
}
