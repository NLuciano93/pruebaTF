using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class CheckProjectCodeExistsValidator : AbstractValidator<CheckProjectCodeExistsRequest>
    {
        public CheckProjectCodeExistsValidator()
        {
            RuleFor(x => x.ProjectCode)
                .NotNull()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.ProjectCodeIsNull)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.ProjectCodeIsEmpty)
                .MaximumLength(10)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidProjectCodeLength);
            ;
        }
    }
}
