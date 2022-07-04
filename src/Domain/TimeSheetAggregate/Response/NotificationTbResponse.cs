namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Response
{
    public class NotificationTbResponse
    {
        public int NotificationsID { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
        public string? CreatedOn { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? Min { get; set; }
    }
}
