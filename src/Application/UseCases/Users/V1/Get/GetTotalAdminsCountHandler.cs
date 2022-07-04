using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Users.V1.Get
{
    public class GetTotalAdminsCountHandler : Handler<GetTotalAdminsCountRequest, int>
    {
        private readonly ILogger<GetTotalAdminsCountHandler> _logger;
        private readonly IService _service;

        public GetTotalAdminsCountHandler(ILogger<GetTotalAdminsCountHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result<int>> Handle(GetTotalAdminsCountRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.GetTotalAdminsCountAsync();

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
