using System;

namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Response
{
    public class UserDateIsUsedResponse
    {
        public int TimeSheetId { get; set; }
        public string? DaysofWeek { get; set; }
        public decimal Hours { get; set; }
        public DateTime Period { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int TimeSheetMasterId { get; set; }
        public decimal? TotalHours { get; set; }
    }
}
