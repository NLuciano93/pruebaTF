using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.DescriptionTb.V1.Insert
{
    public class AddDescriptionTbRequest : Request<DescriptionTbIdResponse>
    {
        public string Description { get; set; } = default!;
        public int ProjectId { get; set; }
        public int TimeSheetMasterId { get; set; }
        public int UserId { get; set; }
    }
}
