using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Roles.V1.Get
{
    public class GetRoleIdbyRolenameHandler : Handler<GetRoleIdbyRolenameRequest, int>
    {
        private readonly ILogger<GetRoleIdbyRolenameHandler> _logger;
        private readonly IService _service;

        public GetRoleIdbyRolenameHandler(ILogger<GetRoleIdbyRolenameHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result<int>> Handle(GetRoleIdbyRolenameRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.GetRoleIdbyRolenameAsync(request.RoleName);

                if (result == 0)
                {
                    _logger.LogInformation(ErrorCatalog.TimeSheet.RoleDoesNotExist.Message);

                    return NotFound(ErrorCatalog.TimeSheet.RoleDoesNotExist);
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
