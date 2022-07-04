using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Get
{
    public class ShowAllTimeSheetsRequest : Request<IPagination<TimeSheetMasterResponse>>
    {
        public int? UserId { get; set; }
        public int? TimeSheetStatus { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public int Offset { get; set; }
    }
}
