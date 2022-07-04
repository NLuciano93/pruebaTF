using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.Expenses.V1.Get;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.Expenses.V1
{
    public partial class ExpensesController
    {
        /// <summary>
        /// Get Expense Audit Count By AdminId
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ExpenseDisplayViewResponse>> GetExpesesCountByAdminIdAsync()
        {
            var result = await Send(new GetExpenseAuditCountByAdminIdRequest(GetUser()));

            return Ok(result);
        }

        /// <summary>
        /// Check ProjectId exists in Expense
        /// </summary>
        /// <returns></returns>
        /// <param name="projectId">ProjectId</param>
        /// <response code="404">
        /// Not Found: indicates that the server can't find the requested resource.
        /// </response>
        [HttpGet("check-project-exists-in-expense")]
        public async Task<ActionResult> CheckProjectIdExistsInExpenseAsync([FromQuery] int projectId)
        {
            var result = await Send(new CheckProjectIdExistsInExpenseRequest(projectId));

            return Ok(result);
        }
    }
}
