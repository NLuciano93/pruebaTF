using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Expenses.V1.Get
{
    public class CheckProjectIdExistsInExpenseValidator : AbstractValidator<CheckProjectIdExistsInExpenseRequest>
    {
        public CheckProjectIdExistsInExpenseValidator()
        {
            RuleFor(x => x)
                .Must(x => x.ProjectId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidProjectId);
        }
    }
}
