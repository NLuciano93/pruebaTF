using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Delete;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.TimeSheets.V1
{
    public partial class TimeSheetsController
    {
        /// <summary>
        /// Delete TimeSheetMaster
        /// </summary>
        /// <param name="timeSheetMasterId">TimeSheetMasterId</param>
        /// <param name="userId">UserId</param>
        /// <returns></returns>
        [HttpDelete("{timeSheetMasterId}")]
        public async Task<ActionResult> DeleteTimeSheetMasterAsync([FromRoute] int timeSheetMasterId, [FromQuery] int userId)
        {
            var result = await Send(new DeleteTimeSheetMasterRequest(timeSheetMasterId, userId));

            return Ok(result);
        }
    }
}
