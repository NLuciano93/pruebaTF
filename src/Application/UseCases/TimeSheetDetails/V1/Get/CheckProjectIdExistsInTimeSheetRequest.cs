using Fusap.Common.Mediator;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheetDetails.V1.Get
{
    public class CheckProjectIdExistsInTimeSheetRequest : Request
    {
        public int ProjectId { get; set; }

        public CheckProjectIdExistsInTimeSheetRequest(int projectId)
        {
            ProjectId = projectId;
        }
    }
}
