using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Users.V1.Get
{
    public class GetAdminDetailsByRegistrationIdValidator : AbstractValidator<GetAdminDetailsByRegistrationIdRequest>
    {
        public GetAdminDetailsByRegistrationIdValidator()
        {
            RuleFor(x => x)
                .Must(x => x.RegistrationId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.RegistrationIdNegative);
        }
    }
}
