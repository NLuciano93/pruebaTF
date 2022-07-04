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

namespace Fusap.TimeSheet.Application.UseCases.NotificationsTb.V1.Get
{
    public class ShowNotificationsTbHandler : Handler<ShowNotificationsTbRequest, IPagination<NotificationTbResponse>>
    {
        private readonly ILogger<ShowNotificationsTbHandler> _logger;
        private readonly IService _service;
        private readonly IMapper _mapper;

        public ShowNotificationsTbHandler(ILogger<ShowNotificationsTbHandler> logger, IService service, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Result<IPagination<NotificationTbResponse>>> Handle(ShowNotificationsTbRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var showNotificationsDto = _mapper.Map<ShowNotificationsDto>(request);

                if (string.IsNullOrEmpty(showNotificationsDto.SortColumn))
                {
                    showNotificationsDto.SortColumn = "Message";
                }

                if (string.IsNullOrEmpty(showNotificationsDto.SortDirection))
                {
                    showNotificationsDto.SortDirection = "ASC";
                }

                var result = await _service.GetNotificationsTbAsync(showNotificationsDto);

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
