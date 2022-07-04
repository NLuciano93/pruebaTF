using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Delete
{
    public class DeleteTimeSheetMasterHandler : Handler<DeleteTimeSheetMasterRequest>
    {
        private readonly ILogger<DeleteTimeSheetMasterHandler> _logger;
        private readonly IService _service;

        public DeleteTimeSheetMasterHandler(ILogger<DeleteTimeSheetMasterHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result> Handle(DeleteTimeSheetMasterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.DeleteAsync(timeSheetMasterId: request.TimeSheetMasterId, userId: request.UserId);
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
