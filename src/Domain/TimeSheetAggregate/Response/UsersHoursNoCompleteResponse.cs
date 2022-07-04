namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Response
{
    public class UsersHoursNoCompleteResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
