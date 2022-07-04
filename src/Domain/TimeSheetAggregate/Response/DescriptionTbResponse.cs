using System;

namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Response
{
    public class DescriptionTbResponse
    {
        public int DescriptionId { get; set; }
        public string Description { get; set; } = default!;
        public int? ProjectId { get; set; }
        public int? TimeSheetMasterId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UserId { get; set; }
    }
}
