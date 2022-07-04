using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Users.V1.Get
{
    public class GetUserDetailsByRegistrationIdHandler : Handler<GetUserDetailsByRegistrationIdRequest, RegistrationViewDetailsResponse>
    {
        private readonly ILogger<GetUserDetailsByRegistrationIdHandler> _logger;
        private readonly IService _service;

        public GetUserDetailsByRegistrationIdHandler(ILogger<GetUserDetailsByRegistrationIdHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result<RegistrationViewDetailsResponse>> Handle(GetUserDetailsByRegistrationIdRequest request, CancellationToken cancellationToken)
        {
            var validations = new GetUserDetailsByRegistrationIdValidator();

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
                var result = await _service.GetUserDetailsByRegistrationIdAsync(request.RegistrationId);

                if (result == null)
                {
                    _logger.LogInformation(ErrorCatalog.TimeSheet.UserDoesNotExist.Message);
                    return NotFound(ErrorCatalog.TimeSheet.UserDoesNotExist);
                }

                return Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
