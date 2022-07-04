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

namespace Fusap.TimeSheet.Application.UseCases.NotificationsTb.V1.Insert
{
    public class AddNotificationsTbHandler : Handler<AddNotificationsTbRequest, NotificationsTbIdResponse>
    {
        private readonly ILogger<AddNotificationsTbHandler> _logger;
        private readonly IService _service;
        private readonly IMapper _mapper;

        public AddNotificationsTbHandler(ILogger<AddNotificationsTbHandler> logger, IService service, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Result<NotificationsTbIdResponse>> Handle(AddNotificationsTbRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var notificationsTb = _mapper.Map<Domain.TimeSheetAggregate.Entities.NotificationsTb>(request);

                notificationsTb.Status = "A";
                notificationsTb.CreatedOn = DateTime.Now;

                var notificationsId = await _service.InsertAsync(notificationsTb: notificationsTb);

                return new NotificationsTbIdResponse() { NotificationsId = notificationsId };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
