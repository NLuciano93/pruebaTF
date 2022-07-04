using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class CheckProjectNameExistsHandler : Handler<CheckProjectNameExistsRequest>
    {
        private readonly ILogger<CheckProjectNameExistsHandler> _logger;
        private readonly IService _service;

        public CheckProjectNameExistsHandler(ILogger<CheckProjectNameExistsHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result> Handle(CheckProjectNameExistsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.CheckProjectNameExistsAsync(request.ProjectName);

                if (!result)
                {
                    _logger.LogInformation(ErrorCatalog.TimeSheet.ProjectNameDoesNotExist.Message);

                    return NotFound(ErrorCatalog.TimeSheet.ProjectNameDoesNotExist);
                }

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
