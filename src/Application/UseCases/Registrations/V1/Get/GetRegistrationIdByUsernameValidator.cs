using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Registrations.V1.Get
{
    public class GetRegistrationIdByUsernameValidator : AbstractValidator<GetRegistrationIdByUsernameRequest>
    {
        public GetRegistrationIdByUsernameValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.UndefinedUsername)
                .Length(6, 20)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidUsernameLength);
        }
    }
}
