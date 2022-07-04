using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Roles.V1.Insert
{
    public class RegistrationValidator : AbstractValidator<AssignRolesRequest>
    {
        public RegistrationValidator()
        {
            RuleFor(x => x.ListOfUsers)
                .NotNull()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.ThereAreNoUsersToAssign);
        }
    }
}
