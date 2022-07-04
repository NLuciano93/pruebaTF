using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.NotificationsTb.V1.Update
{
    public class DisableExistingNotificationsHandler : Handler<DisableExistingNotificationsRequest>
    {
        private readonly ILogger<DisableExistingNotificationsHandler> _logger;
        private readonly IService _service;

        public DisableExistingNotificationsHandler(ILogger<DisableExistingNotificationsHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result> Handle(DisableExistingNotificationsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.DisableExistingNotificationsAsync();

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
