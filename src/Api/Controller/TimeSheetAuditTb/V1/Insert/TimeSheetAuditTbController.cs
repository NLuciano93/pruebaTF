using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.TimeSheetAuditTb.Insert;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.TimeSheetAuditTb.V1
{
    public partial class TimeSheetAuditTbController
    {
        /// <summary>
        /// Insert TimeSheet Audit 
        /// </summary>
        /// <param name="input">Input</param>
        /// <response code="201">
        /// Created: indicates that the request has succeeded and has led to the creation of a resource.
        /// </response>
        [HttpPost]
        public async Task<ActionResult<InsertTimeSheetAuditLogIdResponse>> InsertTimeSheetAuditLog([FromBody] InsertTimeSheetAuditLogRequest input)
        {
            var result = await Send(input);

            return Created(result);
        }
    }
}
