using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.NotificationsTb.V1.Update;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.NotificationsTb.V1
{
    public partial class NotificationsTbController
    {
        /// <summary>
        /// Disable Existing Notification
        /// </summary>
        /// <returns></returns>
        [HttpPost("disable-existing-notification")]
        public async Task<ActionResult> DisableExistingNotificationsAsync()
        {
            var result = await Send(new DisableExistingNotificationsRequest());

            return Ok(result);
        }

        /// <summary>
        /// Desactivate Notification by Id
        /// </summary>
        /// <param name="notificationId">NotificationId</param>
        /// <returns></returns>
        [HttpPost("{notificationId}/desactivate-notification-by-id")]
        public async Task<ActionResult> DeActivateNotificationByIdAsync([FromRoute] int notificationId)
        {
            var result = await Send(new DeActivateNotificationByIdRequest(notificationId));

            return Ok(result);
        }
    }
}
