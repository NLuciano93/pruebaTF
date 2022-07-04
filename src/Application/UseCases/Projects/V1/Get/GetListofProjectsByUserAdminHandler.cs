using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class GetListofProjectsByUserAdminHandler : Handler<GetListofProjectsByUserAdminRequest, ProjectListResponse>
    {
        private readonly ILogger<GetListofProjectsByUserAdminHandler> _logger;
        private readonly IService _service;

        public GetListofProjectsByUserAdminHandler(ILogger<GetListofProjectsByUserAdminHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result<ProjectListResponse>> Handle(GetListofProjectsByUserAdminRequest request, CancellationToken cancellationToken)
        {
            var validations = new GetListofProjectsByUserAdminValidator();

            var results = validations.Validate(request);

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
                var projectListResponse = new ProjectListResponse
                {
                    ListofProjects = await _service.ListofProjectsByUserAdminAsync(request.UserId)
                };

                return projectListResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
