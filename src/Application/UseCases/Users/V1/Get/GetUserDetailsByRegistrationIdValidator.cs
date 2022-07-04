using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Users.V1.Get
{
    public class GetUserDetailsByRegistrationIdValidator : AbstractValidator<GetUserDetailsByRegistrationIdRequest>
    {
        public GetUserDetailsByRegistrationIdValidator()
        {
            RuleFor(x => x)
                .Must(x => x.RegistrationId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.RegistrationIdNegative);
        }
    }
}
