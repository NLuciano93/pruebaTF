using System;
using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheetDetails.V1.Insert
{
    public class AddTimeSheetDetailRequest : Request<TimeSheetDetailsIdResponse>
    {
        public string? DaysofWeek { get; set; }
        public decimal? Hours { get; set; }
        public DateTime? Period { get; set; }
        public int? ProjectId { get; set; }
        public int? UserId { get; set; }
        public int? TimeSheetMasterId { get; set; }
        public decimal? TotalHours { get; set; }
    }
}
