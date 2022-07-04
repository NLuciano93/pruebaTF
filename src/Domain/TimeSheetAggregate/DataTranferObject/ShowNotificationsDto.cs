namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject
{
    public class ShowNotificationsDto
    {
        public string? Message { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public int Offset { get; set; }
    }
}
