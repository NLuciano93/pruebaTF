using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Entities;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Insert
{
    public class AssignProjectToUsersHandler : Handler<AssignProjectToUsersRequest>
    {
        private readonly ILogger<AssignProjectToUsersHandler> _logger;
        private readonly IService _service;
        private readonly IMapper _mapper;

        public AssignProjectToUsersHandler(ILogger<AssignProjectToUsersHandler> logger, IService service, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Result> Handle(AssignProjectToUsersRequest request, CancellationToken cancellationToken)
        {
            var validations = new AssignProjectToUsersValidator();

            var results = validations.Validate(request.AssignProjectToUsersInput);

            if (!results.IsValid)
            {
                var validationErrorDetail = new List<ValidationErrorDetail>();

                foreach (var failure in results.Errors)
                {
                    validationErrorDetail.Add(new ValidationErrorDetail(failure.PropertyName, failure.ErrorCode, failure.ErrorMessage));
                }

                return Invalid(validationErrorDetail.ToArray());
            }

            try
            {
                var selectedCount = (from User in request.AssignProjectToUsersInput.ListOfUsers
                                     where User.SelectedUsers
                                     select User).Count();

                if (selectedCount == 0)
                {
                    return Error(ErrorCatalog.TimeSheet.HaveNotSelectedUsersToAssign);
                }

                var assignProjectToUsersDto = _mapper.Map<AssignProjectToUsersDto>(request.AssignProjectToUsersInput);
                assignProjectToUsersDto.ProjectId = request.ProjectId;

                var exist = await _service.ValidateAssignationAsync(assignProjectToUsersDto);

                if (exist)
                {
                    return Error(ErrorCatalog.TimeSheet.UserHasAlreadyBeenAssigned);
                }

                for (var i = 0; i < request.AssignProjectToUsersInput.ListOfUsers.Count; i++)
                {
                    if (request.AssignProjectToUsersInput.ListOfUsers[i].SelectedUsers)
                    {
                        var assignedProjectToUsers = new AssignedProjectToUsers
                        {
                            AssignedProjectToUsersId = 0,
                            ProjectId = request.ProjectId,
                            CreatedOn = DateTime.Now,
                            CreatedBy = request.AssignProjectToUsersInput.CreatedBy,
                            RegistrationId = request.AssignProjectToUsersInput.ListOfUsers[i].RegistrationId
                        };

                        _service.Insert(assignedProjectToUsers: assignedProjectToUsers);
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
