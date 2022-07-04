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
    public class GetTotalProjectsCountsHandler : Handler<GetTotalProjectsCountsRequest, int>
    {
        private readonly ILogger<GetTotalProjectsCountsHandler> _logger;
        private readonly IService _service;

        public GetTotalProjectsCountsHandler(ILogger<GetTotalProjectsCountsHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result<int>> Handle(GetTotalProjectsCountsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var totalProjectsCounts = await _service.GetTotalProjectsCountsAsync();

                return totalProjectsCounts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
