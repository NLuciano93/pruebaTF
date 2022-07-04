using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.NotificationsTb.V1.Insert;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.NotificationsTb.V1
{
    public partial class NotificationsTbController
    {
        /// <summary>
        /// Add NotificationTb
        /// </summary>
        /// <param name="input">Input</param>
        /// <response code="201">
        /// Created: indicates that the request has succeeded and has led to the creation of a resource.
        /// </response>
        [HttpPost]
        public async Task<ActionResult<NotificationsTbIdResponse>> AddNotificationsTbAsync([FromBody] AddNotificationsTbRequest input)
        {
            var result = await Send(input);

            return Created(result);
        }
    }
}
