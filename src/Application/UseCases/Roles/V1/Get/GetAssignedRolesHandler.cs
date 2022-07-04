using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Roles.V1.Get
{
    public class GetAssignedRolesHandler : Handler<GetAssignedRolesRequest, AssignedRolesResponse>
    {
        private readonly ILogger<GetAssignedRolesHandler> _logger;
        private readonly IService _service;

        public GetAssignedRolesHandler(ILogger<GetAssignedRolesHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result<AssignedRolesResponse>> Handle(GetAssignedRolesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var assignedRolesResponse = new AssignedRolesResponse
                {
                    ListofAdmins = await _service.ListOfAdminsAsync(),
                    ListofUser = await _service.ListOfUserAsync()
                };

                return assignedRolesResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
