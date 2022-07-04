using Fusap.Common.Mediator;

namespace Fusap.TimeSheet.Application.UseCases.NotificationsTb.V1.Update
{
    public class DeActivateNotificationByIdRequest : Request
    {
        public int NotificationId { get; set; }

        public DeActivateNotificationByIdRequest(int notificationId)
        {
            NotificationId = notificationId;
        }
    }
}
