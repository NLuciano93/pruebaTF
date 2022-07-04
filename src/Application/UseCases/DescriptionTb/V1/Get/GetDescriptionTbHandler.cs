using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.DescriptionTb.V1.Get
{
    public class GetDescriptionTbHandler : Handler<GetDescriptionTbRequest, DescriptionTbResponse>
    {
        private readonly ILogger<GetDescriptionTbHandler> _logger;
        private readonly IService _service;

        public GetDescriptionTbHandler(ILogger<GetDescriptionTbHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result<DescriptionTbResponse>> Handle(GetDescriptionTbRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.GetDescriptionTbAsync(request.ProjectId, request.TimeSheetMasterId);

                if (result == null)
                {
                    _logger.LogInformation(ErrorCatalog.TimeSheet.DescriptionTbDoesNotExist.Message);
                    return NotFound(ErrorCatalog.TimeSheet.DescriptionTbDoesNotExist);
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
