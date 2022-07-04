using System.Text.RegularExpressions;
using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Registrations.V1.Insert
{
    public class AddRegistrationValidator : AbstractValidator<AddRegistrationRequest>
    {
        private static readonly Regex s_definitionMobilenoRegex = new Regex(@"^(\d{10})$", RegexOptions.Compiled);
        private static readonly Regex s_definitionEmailIdRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.Compiled);

        public AddRegistrationValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.UndefinedName)
                .Length(1, 100)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidNameLength);

            RuleFor(x => x.Mobileno)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.UndefinedMobileno)
                .Matches(s_definitionMobilenoRegex)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidMobileno);

            RuleFor(x => x.EmailId)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.UndefinedEmailId)
                .Matches(s_definitionEmailIdRegex)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidEmailId);

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.UndefinedUsername)
                .Length(6, 20)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidUsernameLength);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.UndefinedPassword)
                .MinimumLength(6)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidPasswordLength)
                .MaximumLength(100)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidPasswordLength);

            RuleFor(x => x.ConfirmPassword)
                .Equal(y => y.Password)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.PasswordsDidNotMatch);
        }
    }
}
