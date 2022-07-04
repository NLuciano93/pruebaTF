using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Update;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.TimeSheets.V1
{
    public partial class TimeSheetsController
    {
        /// <summary>
        /// Update TimeSheet Status
        /// </summary>
        /// <param name="timeSheetMasterId">TimeSheetMasterId</param>
        /// <param name="input">New TimeSheet Status</param>
        /// <returns></returns>
        [HttpPost("{timeSheetMasterId}/update-status")]
        public async Task<ActionResult> UpdateTimeSheetStatusAsync(
            [FromRoute] int timeSheetMasterId,
            [FromBody] UpdateTimeSheetStatusInput input)
        {
            var result = await Send(new UpdateTimeSheetStatusRequest(timeSheetMasterId, input));

            return Ok(result);
        }

        /// <summary>
        /// Update TimeSheetMaster
        /// </summary>
        /// <param name="timeSheetMasterId">TimeSheetMasterId</param>
        /// <param name="input">Input</param>
        /// <returns></returns>
        [HttpPost("{timeSheetMasterId}")]
        public async Task<ActionResult> UpdateTimeSheetMasterAsync(
            [FromRoute] int timeSheetMasterId,
            [FromBody] UpdateTimeSheetMasterInput input)
        {
            var result = await Send(new UpdateTimeSheetMasterRequest(timeSheetMasterId, input));

            return Ok(result);
        }
    }
}
