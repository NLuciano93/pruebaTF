using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.TimeSheetDetails.V1.Get;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.TimeSheetDetails.V1
{
    public partial class TimeSheetDetailsController
    {
        /// <summary>
        /// Check is date alrady used
        /// </summary>
        /// <param name="input">Request for query input</param>
        /// <returns></returns>
        [HttpGet("check-date-is-alredy-used")]
        public async Task<ActionResult<UserDateIsUsedResponse>> GetUsersDateIsUsedAsync([FromQuery] GetUserDateIsUsedInput input)
        {
            var request = Map<GetUserDateIsUsedRequest>(input);

            var result = await Send(request);

            return OkAs<UserDateIsUsedResponse>(result);
        }

        /// <summary>
        /// Check ProjectId exists in TimeSheet
        /// </summary>
        /// <returns></returns>
        /// <param name="projectId">ProjectId</param>
        /// <response code="404">
        /// Not Found: indicates that the server can't find the requested resource.
        /// </response>
        [HttpGet("check-project-exists-in-timesheet")]
        public async Task<ActionResult> CheckProjectIdExistsInTimeSheetAsync([FromQuery] int projectId)
        {
            var result = await Send(new CheckProjectIdExistsInTimeSheetRequest(projectId));

            return Ok(result);
        }
    }
}
