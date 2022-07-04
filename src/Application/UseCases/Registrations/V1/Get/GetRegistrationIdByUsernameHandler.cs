using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Registrations.V1.Get
{
    public class GetRegistrationIdByUsernameHandler : Handler<GetRegistrationIdByUsernameRequest, int>
    {
        private readonly ILogger<GetRegistrationIdByUsernameHandler> _logger;
        private readonly IService _service;

        public GetRegistrationIdByUsernameHandler(ILogger<GetRegistrationIdByUsernameHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result<int>> Handle(GetRegistrationIdByUsernameRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var registrationId = await _service.GetRegistrationIdByRegistrationUsernameAsync(request.UserName);

                if (registrationId == 0)
                {
                    _logger.LogInformation(ErrorCatalog.TimeSheet.RegistrationDoesNotExist.Message);
                    return NotFound(ErrorCatalog.TimeSheet.RegistrationDoesNotExist);
                }

                return registrationId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
