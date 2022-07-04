using System;
using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Users.V1.Get
{
    public class GetUsersHoursNoCompleteValidator : AbstractValidator<GetUsersHoursNoCompleteRequest>
    {
        public GetUsersHoursNoCompleteValidator()
        {
            RuleFor(x => x)
                .Must(x => x.Offset > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidOffset);

            RuleFor(x => x)
                .Must(x => x.Date > new DateTime(2020, 1, 1))
                .WithErrorCatalog(ErrorCatalog.TimeSheet.DateOutOfRange);
        }
    }
}
