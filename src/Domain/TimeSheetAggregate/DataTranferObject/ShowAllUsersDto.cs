namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject
{
    public class ShowAllUsersDto
    {
        public int RoleId { get; set; }
        public string? Name { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public int Offset { get; set; }
    }
}
