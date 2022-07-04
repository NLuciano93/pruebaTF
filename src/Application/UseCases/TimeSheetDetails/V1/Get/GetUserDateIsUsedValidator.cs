using System;
using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheetDetails.V1.Get
{
    public class GetUserDateIsUsedValidator : AbstractValidator<GetUserDateIsUsedRequest>
    {
        public GetUserDateIsUsedValidator()
        {
            RuleFor(x => x)
                .Must(x => x.UserId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidUserId);

            RuleFor(x => x)
               .Must(x => x.Period > new DateTime(2020, 1, 1))
               .WithErrorCatalog(ErrorCatalog.TimeSheet.DateOutOfRange);
        }
    }
}
