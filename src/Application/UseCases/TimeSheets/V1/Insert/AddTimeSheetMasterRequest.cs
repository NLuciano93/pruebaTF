using System;
using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Insert
{
    public class AddTimeSheetMasterRequest : Request<TimeSheetMasterIdResponse>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? TotalHours { get; set; }
        public int UserId { get; set; }
        public string? Comment { get; set; }
        public int? TimeSheetStatus { get; set; }
    }
}
