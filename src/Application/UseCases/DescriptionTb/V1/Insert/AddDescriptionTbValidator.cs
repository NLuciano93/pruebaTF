using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.DescriptionTb.V1.Insert
{
    public class AddDescriptionTbValidator : AbstractValidator<AddDescriptionTbRequest>
    {
        public AddDescriptionTbValidator()
        {
            RuleFor(x => x)
                .Must(x => x.ProjectId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidProjectId);

            RuleFor(x => x)
                .Must(x => x.TimeSheetMasterId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidTimeSheetMasterId);

            RuleFor(x => x)
                .Must(x => x.UserId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidUserId);

            RuleFor(x => x.Description)
                .NotNull()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.DescriptionIsNull)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.UndefinedDescription)
                .MaximumLength(100)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidDescriptionLength);
        }
    }
}
