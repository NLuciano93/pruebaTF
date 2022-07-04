using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.DescriptionTb.V1.Get
{
    public class GetDescriptionTbRequest : Request<DescriptionTbResponse>
    {
        public int ProjectId { get; set; }
        public int TimeSheetMasterId { get; set; }
    }
}
