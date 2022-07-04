namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Response
{
    public class RegistrationViewDetailsResponse
    {
        public string? EmployeeId { get; set; }
        public int RegistrationId { get; set; }
        public string? Name { get; set; }
        public string? Mobileno { get; set; }
        public string? EmailId { get; set; }
        public string? UserName { get; set; } = default!;
        public string? Birthdate { get; set; }
        public string? DateOfJoining { get; set; }
        public string? Gender { get; set; }
    }
}
