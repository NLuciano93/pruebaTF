using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.NotificationsTb.V1.Get
{
    public class ShowNotificationsTbRequest : Request<IPagination<NotificationTbResponse>>
    {
        public string? Message { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public int Offset { get; set; }
    }
}
