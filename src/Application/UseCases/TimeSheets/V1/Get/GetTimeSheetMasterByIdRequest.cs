using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Get
{
    public class GetTimeSheetMasterByIdRequest : Request<TimeSheetMasterResponse>
    {
        public GetTimeSheetMasterByIdRequest(int timeSheetMasterId)
        {
            TimeSheetMasterId = timeSheetMasterId;
        }

        public int TimeSheetMasterId { get; set; }
    }
}
