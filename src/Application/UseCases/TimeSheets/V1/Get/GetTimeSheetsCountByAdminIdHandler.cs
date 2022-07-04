using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Get
{
    public class GetTimeSheetsCountByAdminIdHandler : Handler<GetTimeSheetsCountByAdminIdRequest, TimeSheetDisplayViewResponse>
    {
        private readonly ILogger<GetTimeSheetsCountByAdminIdHandler> _logger;
        private readonly IService _service;

        public GetTimeSheetsCountByAdminIdHandler(ILogger<GetTimeSheetsCountByAdminIdHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result<TimeSheetDisplayViewResponse>> Handle(GetTimeSheetsCountByAdminIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.GetTimeSheetsCountByAdminIdAsync(request.AdminId);

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
