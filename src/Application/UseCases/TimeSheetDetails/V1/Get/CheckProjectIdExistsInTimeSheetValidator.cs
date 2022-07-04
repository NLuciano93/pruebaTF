using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheetDetails.V1.Get
{
    public class CheckProjectIdExistsInTimeSheetValidator : AbstractValidator<CheckProjectIdExistsInTimeSheetRequest>
    {
        public CheckProjectIdExistsInTimeSheetValidator()
        {
            RuleFor(x => x)
                .Must(x => x.ProjectId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidProjectId);
        }
    }
}
