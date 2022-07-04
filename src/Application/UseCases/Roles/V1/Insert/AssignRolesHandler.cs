using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Entities;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Roles.V1.Insert
{
    public class AssignRolesHandler : Handler<AssignRolesRequest>
    {
        private readonly ILogger<AssignRolesHandler> _logger;
        private readonly IService _service;

        public AssignRolesHandler(ILogger<AssignRolesHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result> Handle(AssignRolesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var selectedCount = (from User in request.ListOfUsers
                                     where User.SelectedUsers
                                     select User).Count();

                if (selectedCount == 0)
                {
                    return Error(ErrorCatalog.TimeSheet.HaveNotSelectedUsersToAssign);
                }

                for (var i = 0; i < request.ListOfUsers.Count; i++)
                {
                    if (request.ListOfUsers[i].SelectedUsers)
                    {
                        var assignedRoles = new AssignedRoles
                        {
                            AssignedRolesId = 0,
                            AssignToAdmin = request.RegistrationID,
                            CreatedOn = DateTime.Now,
                            CreatedBy = request.CreatedBy,
                            Status = "A",
                            RegistrationId = request.ListOfUsers[i].RegistrationID
                        };

                        _service.Insert(assignedRoles: assignedRoles);
                    }
                }

                return await Task.FromResult(Success());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
