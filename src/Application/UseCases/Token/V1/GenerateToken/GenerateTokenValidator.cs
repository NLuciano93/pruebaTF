using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Token.V1.GenerateToken
{
    public class GenerateTokenValidator : AbstractValidator<GenerateTokenRequest>
    {
        public GenerateTokenValidator()
        {
            RuleFor(x => x)
                .Must(x => x.Username != null || x.Password != null)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidModel);

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.TokenInvalidCredentials);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.TokenInvalidCredentials);
        }
    }
}
