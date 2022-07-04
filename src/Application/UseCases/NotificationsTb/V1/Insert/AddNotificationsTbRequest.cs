using System;
using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.NotificationsTb.V1.Insert
{
    public class AddNotificationsTbRequest : Request<NotificationsTbIdResponse>
    {
        public string? Message { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
