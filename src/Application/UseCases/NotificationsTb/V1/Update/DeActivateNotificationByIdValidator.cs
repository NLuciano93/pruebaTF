using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.NotificationsTb.V1.Update
{
    public class DeActivateNotificationByIdValidator : AbstractValidator<DeActivateNotificationByIdRequest>
    {
        public DeActivateNotificationByIdValidator()
        {
            RuleFor(x => x)
                .Must(x => x.NotificationId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidNotificationId);
        }
    }
}
