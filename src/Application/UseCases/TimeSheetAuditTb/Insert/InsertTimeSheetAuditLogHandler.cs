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

namespace Fusap.TimeSheet.Application.UseCases.TimeSheetAuditTb.Insert
{
    public class InsertTimeSheetAuditLogHandler : Handler<InsertTimeSheetAuditLogRequest, InsertTimeSheetAuditLogIdResponse>
    {
        private readonly ILogger<InsertTimeSheetAuditLogHandler> _logger;
        private readonly IService _service;
        private readonly IMapper _mapper;

        public InsertTimeSheetAuditLogHandler(ILogger<InsertTimeSheetAuditLogHandler> logger, IService service, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Result<InsertTimeSheetAuditLogIdResponse>> Handle(InsertTimeSheetAuditLogRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var timeSheetAuditTb = _mapper.Map<Domain.TimeSheetAggregate.Entities.TimeSheetAuditTb>(request);

                timeSheetAuditTb.ProcessedDate = DateTime.Now;
                timeSheetAuditTb.CreatedOn = DateTime.Now;

                var approvalTimeSheetLogId = await _service.InsertAsync(timeSheetAuditTb: timeSheetAuditTb);

                return new InsertTimeSheetAuditLogIdResponse() { ApprovalTimeSheetLogId = approvalTimeSheetLogId };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
