namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject
{
    public class ShowAllTimeSheetsDto
    {
        public int? UserId { get; set; }
        public int? TimeSheetStatus { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public int Offset { get; set; }
    }
}
