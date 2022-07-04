using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class ShowAllProjectsHandler : Handler<ShowAllProjectsRequest, IPagination<ProjectMasterResponse>>
    {
        private readonly ILogger<ShowAllProjectsHandler> _logger;
        private readonly IService _service;
        private readonly IMapper _mapper;

        public ShowAllProjectsHandler(ILogger<ShowAllProjectsHandler> logger, IService service, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Result<IPagination<ProjectMasterResponse>>> Handle(ShowAllProjectsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var showAllProjectsDto = _mapper.Map<ShowAllProjectsDto>(request);

                if (string.IsNullOrEmpty(showAllProjectsDto.SortColumn))
                {
                    showAllProjectsDto.SortColumn = "ProjectName";
                }

                if (string.IsNullOrEmpty(showAllProjectsDto.SortDirection))
                {
                    showAllProjectsDto.SortDirection = "ASC";
                }

                var result = await _service.GetAllProjectsAsync(showAllProjectsDto);

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
