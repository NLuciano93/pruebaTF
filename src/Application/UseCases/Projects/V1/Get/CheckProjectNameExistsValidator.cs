using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class CheckProjectNameExistsValidator : AbstractValidator<CheckProjectNameExistsRequest>
    {
        public CheckProjectNameExistsValidator()
        {
            RuleFor(x => x.ProjectName)
                .NotNull()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.ProjectNameIsNull)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.ProjectNameIsEmpty)
                .MaximumLength(100)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidProjectNameLength);
        }
    }
}
