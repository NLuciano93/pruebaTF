using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Insert
{
    public class AssignProjectToUsersValidator : AbstractValidator<AssignProjectToUsersInput>
    {
        public AssignProjectToUsersValidator()
        {
            RuleFor(x => x.ListOfUsers)
                .NotNull()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.ThereAreNoUsersToAssign);
        }
    }
}
