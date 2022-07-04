using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheetDetails.V1.Insert
{
    public class AddTimeSheetDetailHandler : Handler<AddTimeSheetDetailRequest, TimeSheetDetailsIdResponse>
    {
        private readonly ILogger<AddTimeSheetDetailHandler> _logger;
        private readonly IService _service;
        private readonly IMapper _mapper;

        public AddTimeSheetDetailHandler(ILogger<AddTimeSheetDetailHandler> logger, IService service, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Result<TimeSheetDetailsIdResponse>> Handle(AddTimeSheetDetailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var timeSheetDetail = _mapper.Map<Domain.TimeSheetAggregate.Entities.TimeSheetDetails>(request);
                timeSheetDetail.CreatedOn = DateTime.Now;

                var timeSheetId = await _service.InsertAsync(timeSheetDetails: timeSheetDetail);

                return new TimeSheetDetailsIdResponse() { TimeSheetId = timeSheetId };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
