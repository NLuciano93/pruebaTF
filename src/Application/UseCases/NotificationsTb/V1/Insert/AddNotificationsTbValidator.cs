using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.NotificationsTb.V1.Insert
{
    public class AddNotificationsTbValidator : AbstractValidator<AddNotificationsTbRequest>
    {
        public AddNotificationsTbValidator()
        {
            RuleFor(x => x.Message)
                .MaximumLength(50)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidMessageLength);
        }
    }
}
