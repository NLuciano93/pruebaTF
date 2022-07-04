using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.Expenses.V1.Get
{
    public class GetExpenseAuditCountByAdminIdRequest : Request<ExpenseDisplayViewResponse>
    {
        public int AdminId { get; set; }

        public GetExpenseAuditCountByAdminIdRequest(int adminId)
        {
            AdminId = adminId;
        }
    }
}
