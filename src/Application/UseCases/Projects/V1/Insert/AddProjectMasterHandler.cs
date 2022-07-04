using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Entities;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Insert
{
    public class AddProjectMasterHandler : Handler<AddProjectMasterRequest, ProjectIdResponse>
    {
        private readonly ILogger<AddProjectMasterHandler> _logger;
        private readonly IService _service;
        private readonly IMapper _mapper;

        public AddProjectMasterHandler(ILogger<AddProjectMasterHandler> logger, IService service, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Result<ProjectIdResponse>> Handle(AddProjectMasterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var projectMaster = _mapper.Map<ProjectMaster>(request);

                var projectId = await _service.InsertAsync(projectMaster: projectMaster);

                return new ProjectIdResponse() { ProjectId = projectId };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
