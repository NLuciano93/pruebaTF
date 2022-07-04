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

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Insert
{
    public class AddTimeSheetMasterHandler : Handler<AddTimeSheetMasterRequest, TimeSheetMasterIdResponse>
    {
        private readonly ILogger<AddTimeSheetMasterHandler> _logger;
        private readonly IService _service;
        private readonly IMapper _mapper;

        public AddTimeSheetMasterHandler(ILogger<AddTimeSheetMasterHandler> logger, IService service, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Result<TimeSheetMasterIdResponse>> Handle(AddTimeSheetMasterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var timeSheetMaster = _mapper.Map<TimeSheetMaster>(request);
                timeSheetMaster.CreatedOn = DateTime.Now;

                var timeSheetId = await _service.InsertAsync(timeSheetMaster: timeSheetMaster);

                return new TimeSheetMasterIdResponse() { TimeSheetMasterId = timeSheetId };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
