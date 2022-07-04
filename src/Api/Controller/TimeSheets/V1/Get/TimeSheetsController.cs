using System.Threading.Tasks;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Get;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.TimeSheets.V1
{
    public partial class TimeSheetsController
    {
        /// <summary>
        /// Get TimeSheetMaster by Id
        /// </summary>
        /// <param name="timeSheetMasterId">TimeSheetMasterId</param>
        /// <returns></returns>
        /// <response code="404">
        /// Not Found: indicates that the server can't find the requested resource.
        /// </response>
        [HttpGet("{timeSheetMasterId}")]
        public async Task<ActionResult<TimeSheetMasterResponse>> GetTimeSheetMasterByIdAsync([FromRoute] int timeSheetMasterId)
        {
            var result = await Send(new GetTimeSheetMasterByIdRequest(timeSheetMasterId));

            return Ok(result);
        }

        /// <summary>
        /// Get TimeSheetsCount by AdminId
        /// </summary>
        /// <returns></returns>
        [HttpGet("count-by-adminid")]
        public async Task<ActionResult<TimeSheetDisplayViewResponse>> GetTimeSheetsCountByAdminIdAsyn()
        {
            var result = await Send(new GetTimeSheetsCountByAdminIdRequest(GetUser()));

            return Ok(result);
        }

        /// <summary>
        /// Get TimeSheetsCount by UserId
        /// </summary>
        /// <returns></returns>
        [HttpGet("count-by-userid")]
        public async Task<ActionResult<TimeSheetDisplayViewResponse>> GetTimeSheetsCountByUserIdAsync()
        {
            var result = await Send(new GetTimeSheetsCountByUserIdRequest(GetUser()));

            return Ok(result);
        }

        /// <summary>
        /// Show all TimeSheets
        /// </summary>
        /// <param name="input">Request for query input</param>
        /// <returns></returns>
        [HttpGet("all-timesheets")]
        public async Task<ActionResult<Pagination<TimeSheetMasterResponse>>> ShowAllTimeSheetsAsync([FromQuery] ShowAllTimeSheetsInput input)
        {
            var request = Map<ShowAllTimeSheetsRequest>(input);

            var result = await Send(request);

            return OkAs<Pagination<TimeSheetMasterResponse>>(result);
        }
    }
}
