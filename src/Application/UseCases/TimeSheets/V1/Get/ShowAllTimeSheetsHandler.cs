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

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Get
{
    public class ShowAllTimeSheetsHandler : Handler<ShowAllTimeSheetsRequest, IPagination<TimeSheetMasterResponse>>
    {
        private readonly ILogger<ShowAllTimeSheetsHandler> _logger;
        private readonly IService _service;
        private readonly IMapper _mapper;

        public ShowAllTimeSheetsHandler(ILogger<ShowAllTimeSheetsHandler> logger, IService service, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Result<IPagination<TimeSheetMasterResponse>>> Handle(ShowAllTimeSheetsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var showAllTimeSheetsDto = _mapper.Map<ShowAllTimeSheetsDto>(request);

                if (string.IsNullOrEmpty(showAllTimeSheetsDto.SortColumn))
                {
                    showAllTimeSheetsDto.SortColumn = "UserID";
                }

                if (string.IsNullOrEmpty(showAllTimeSheetsDto.SortDirection))
                {
                    showAllTimeSheetsDto.SortDirection = "ASC";
                }

                var result = await _service.GetAllTimeSheetsAsync(showAllTimeSheetsDto);

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
