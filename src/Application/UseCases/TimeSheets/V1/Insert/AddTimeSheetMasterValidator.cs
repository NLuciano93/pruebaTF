using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Insert
{
    public class AddTimeSheetMasterValidator : AbstractValidator<AddTimeSheetMasterRequest>
    {
        public AddTimeSheetMasterValidator()
        {
            RuleFor(x => x)
                .Must(x => x.UserId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidUserId);

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
