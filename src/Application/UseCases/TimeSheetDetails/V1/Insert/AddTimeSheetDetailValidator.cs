using System;
using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheetDetails.V1.Insert
{
    public class AddTimeSheetDetailValidator : AbstractValidator<AddTimeSheetDetailRequest>
    {
        public AddTimeSheetDetailValidator()
        {
            When(x => x.UserId != null, () =>
            {
                RuleFor(x => x)
                .Must(x => x.UserId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidUserId);
            });

            When(x => x.ProjectId != null, () =>
            {
                RuleFor(x => x)
                .Must(x => x.TimeSheetMasterId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidProjectId);
            });

            When(x => x.ProjectId != null, () =>
            {
                RuleFor(x => x)
                .Must(x => x.TimeSheetMasterId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidTimeSheetMasterId);
            });

            When(x => x.Period != null, () =>
            {
                RuleFor(x => x)
               .Must(x => x.Period > new DateTime(2020, 1, 1))
               .WithErrorCatalog(ErrorCatalog.TimeSheet.DateOutOfRange);
            });
        }
    }
}
