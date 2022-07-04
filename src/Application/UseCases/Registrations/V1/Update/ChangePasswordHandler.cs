using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Application.Util;
using Fusap.TimeSheet.Data;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Registrations.V1.Update
{
    public class ChangePasswordHandler : Handler<ChangePasswordRequest>
    {
        private readonly ILogger<ChangePasswordHandler> _logger;
        private readonly IService _service;

        public ChangePasswordHandler(ILogger<ChangePasswordHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var validations = new ChangePasswordValidator();

            var results = validations.Validate(request);

            if (!results.IsValid)
            {
                var validationErrorDetail = new List<ValidationErrorDetail>();

                foreach (var failure in results.Errors)
                {
                    validationErrorDetail.Add(new ValidationErrorDetail(failure.PropertyName, failure.ErrorCode, failure.ErrorMessage));
                }

                return Invalid(validationErrorDetail.ToArray());
            }

            try
            {
                var storedPassword = await _service.GetStoredPasswordAsync(request.RegistrationId);

                if (storedPassword != EncryptionLibrary.EncryptText(request.OldPassword))
                {
                    _logger.LogInformation(ErrorCatalog.TimeSheet.PasswordsDidNotMatch.Message);
                    return Invalid(ErrorCatalog.TimeSheet.PasswordsDidNotMatch);
                }

                await _service.UpdatePasswordAsync(request.RegistrationId, EncryptionLibrary.EncryptText(request.NewPassword));

                return Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
