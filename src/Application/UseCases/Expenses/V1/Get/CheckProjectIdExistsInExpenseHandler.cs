using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Expenses.V1.Get
{
    public class CheckProjectIdExistsInExpenseHandler : Handler<CheckProjectIdExistsInExpenseRequest>
    {
        private readonly ILogger<CheckProjectIdExistsInExpenseHandler> _logger;
        private readonly IService _service;

        public CheckProjectIdExistsInExpenseHandler(ILogger<CheckProjectIdExistsInExpenseHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result> Handle(CheckProjectIdExistsInExpenseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.CheckProjectIdExistsInExpenseAsync(request.ProjectId);

                if (!result)
                {
                    _logger.LogInformation(ErrorCatalog.TimeSheet.ProjectDoesNotExist.Message);

                    return NotFound(ErrorCatalog.TimeSheet.ProjectDoesNotExist);
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
