using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheetDetails.V1.Get
{
    public class GetUserDateIsUsedHandler : Handler<GetUserDateIsUsedRequest, UserDateIsUsedResponse>
    {
        private readonly ILogger<GetUserDateIsUsedHandler> _logger;
        private readonly IService _service;

        public GetUserDateIsUsedHandler(ILogger<GetUserDateIsUsedHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result<UserDateIsUsedResponse>> Handle(GetUserDateIsUsedRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.GetUsersDateIsUsedAsync(request.Period, request.UserId);

                if (result == null)
                {
                    _logger.LogInformation(ErrorCatalog.TimeSheet.DateIsNotUsedForUserId.Message);

                    return NotFound(ErrorCatalog.TimeSheet.DateIsNotUsedForUserId);
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
