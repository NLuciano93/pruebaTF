using System.Threading.Tasks;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.UseCases.NotificationsTb.V1.Get;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.NotificationsTb.V1
{
    public partial class NotificationsTbController
    {
        /// <summary>
        /// Show NotificationsTb
        /// </summary>
        /// <param name="input">Request for query input</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Pagination<NotificationTbResponse>>> ShowNotificationsTbAsync([FromQuery] ShowNotificationsTbInput input)
        {
            var request = Map<ShowNotificationsTbRequest>(input);

            var result = await Send(request);

            return OkAs<Pagination<NotificationTbResponse>>(result);
        }
    }
}
