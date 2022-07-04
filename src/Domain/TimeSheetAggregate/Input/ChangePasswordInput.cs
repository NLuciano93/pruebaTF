namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Input
{
    public class ChangePasswordInput
    {
        public string OldPassword { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
    }
}
