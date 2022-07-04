using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Roles.V1.Get
{
    public class GetRoleIdbyRolenameValidator : AbstractValidator<GetRoleIdbyRolenameRequest>
    {
        public GetRoleIdbyRolenameValidator()
        {
            RuleFor(x => x.RoleName)
                .NotNull()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.RoleNameIsNull)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.RoleNameIsEmpty)
                .MaximumLength(100)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidRoleNameLength);
        }
    }
}
