namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Response
{
    public class RegistrationSummaryResponse
    {
        public int RegistrationId { get; set; }
        public string Name { get; set; } = default!;
        public string Mobileno { get; set; } = default!;
        public string EmailId { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string AssignToAdmin { get; set; } = default!;
    }
}
