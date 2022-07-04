using Fusap.Common.Mediator;

namespace Fusap.TimeSheet.Application.UseCases.Expenses.V1.Get
{
    public class CheckProjectIdExistsInExpenseRequest : Request
    {
        public int ProjectId { get; set; }

        public CheckProjectIdExistsInExpenseRequest(int projectId)
        {
            ProjectId = projectId;
        }
    }
}
