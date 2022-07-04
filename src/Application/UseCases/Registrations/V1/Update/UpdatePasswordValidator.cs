using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Registrations.V1.Update
{
    public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordRequest>
    {
        public UpdatePasswordValidator()
        {
            RuleFor(x => x)
                .Must(x => x.RegistrationId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.RegistrationIdNegative);

            RuleFor(x => x.Password)
                .NotNull()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.UndefinedPassword)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.UndefinedPassword)
                .MinimumLength(6)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidPasswordLength)
                .MaximumLength(100)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidPasswordLength);
        }
    }
}
