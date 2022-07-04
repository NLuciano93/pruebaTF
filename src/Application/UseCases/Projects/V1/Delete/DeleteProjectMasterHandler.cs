using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Delete
{
    public class DeleteProjectMasterHandler : Handler<DeleteProjectMasterRequest>
    {
        private readonly ILogger<DeleteProjectMasterHandler> _logger;
        private readonly IService _service;

        public DeleteProjectMasterHandler(ILogger<DeleteProjectMasterHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result> Handle(DeleteProjectMasterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var isExistsInTimesheet = await _service.CheckProjectIdExistsInTimesheetAsync(projectId: request.ProjectId);
                var isExistsInExpense = await _service.CheckProjectIdExistsInExpenseAsync(projectId: request.ProjectId);

                if (!isExistsInTimesheet && !isExistsInExpense)
                {
                    await _service.DeleteAsync(projectId: request.ProjectId);
                    return Success();
                }

                return Conflict(ErrorCatalog.TimeSheet.ProjectIsInUse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
