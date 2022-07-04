using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Token.V1.GetById
{
    public class GetUserHandler : Handler<GetUserRequest, UserDetailResponse>
    {
        private readonly ILogger<GetUserHandler> _logger;
        private readonly IService _service;

        public GetUserHandler(ILogger<GetUserHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result<UserDetailResponse>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.GetUserAsync(request.Username);

                if (result == null)
                {
                    _logger.LogInformation(ErrorCatalog.TimeSheet.UserDoesNotExist.Message);
                    return NotFound(ErrorCatalog.TimeSheet.UserDoesNotExist);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
