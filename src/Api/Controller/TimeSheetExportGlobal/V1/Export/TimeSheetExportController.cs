using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.TimeSheetExportGlobal.V1.Export;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.TimeSheetExportGlobal.V1
{
    public partial class TimeSheetExportController
    {
        /// <summary>
        /// Export a global timesheet from specific month and year
        /// </summary>
        /// <param name="period">Period</param>
        /// <response code="200">
        /// Ok: indicates that the request has succeeded
        /// </response>
        [HttpPost]
        public async Task<ActionResult<ExportGlobalTimeSheetResponse>> ExportTimeSheetAsync([FromBody] ExportGlobalTimeSheetRequest period)
        {
            var result = await Send(period);

            return Ok(result);
        }
    }
}
