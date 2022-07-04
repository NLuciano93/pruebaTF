namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Input
{
    public class UpdateTimeSheetStatusInput
    {
        public int TimeSheetStatus { get; set; }
        public string? Comment { get; set; }
    }
}
