using System;
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
    public class GetListOfProjectsHandler : Handler<GetListOfProjectsRequest, ProjectListResponse>
    {
        private readonly ILogger<GetListOfProjectsHandler> _logger;
        private readonly IService _service;

        public GetListOfProjectsHandler(ILogger<GetListOfProjectsHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result<ProjectListResponse>> Handle(GetListOfProjectsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var projectListResponse = new ProjectListResponse
                {
                    ListofProjects = await _service.ListOfProjectsAsync()
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
