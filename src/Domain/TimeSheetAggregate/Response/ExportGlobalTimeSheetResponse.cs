namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Response
{
    public class ExportGlobalTimeSheetResponse
    {
        public dynamic GlobalTimesheetList { get; set; } = default!;

        public ExportGlobalTimeSheetResponse()
        {

        }
    }
}
