using System;

namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Response
{
    public class TimeSheetMasterResponse
    {
        public int TimeSheetMasterId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? TotalHours { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? Comment { get; set; }
        public int? TimeSheetStatus { get; set; }
    }
}
