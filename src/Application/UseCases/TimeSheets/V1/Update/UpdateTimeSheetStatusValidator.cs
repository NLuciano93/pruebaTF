using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Update
{
    public class UpdateTimeSheetStatusValidator : AbstractValidator<UpdateTimeSheetStatusRequest>
    {
        public UpdateTimeSheetStatusValidator()
        {
            RuleFor(x => x)
                .Must(x => x.TimeSheetMasterId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.RegistrationIdNegative);

            RuleFor(x => x)
                .Must(x => x.TimeSheetStatus > 0 && x.TimeSheetStatus < 4)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidStatus);

            When(x => x.Comment != null, () =>
            {
                RuleFor(x => x.Comment)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.UndefinedComment)
                .MaximumLength(100)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidCommentLength);
            });
        }
    }
}
