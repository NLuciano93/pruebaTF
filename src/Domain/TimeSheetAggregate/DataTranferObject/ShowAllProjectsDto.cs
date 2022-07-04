namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject
{
    public class ShowAllProjectsDto
    {
        public string? ProjectCode { get; set; }
        public string? ProjectName { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public int Offset { get; set; }
    }
}
