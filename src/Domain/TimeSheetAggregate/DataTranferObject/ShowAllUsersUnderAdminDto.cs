namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject
{
    public class ShowAllUsersUnderAdminDto
    {
        public int AdminUserId { get; set; }
        public string? Name { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public int Offset { get; set; }
    }
}
