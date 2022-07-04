using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Get
{
    public class GetTimeSheetsCountByUserIdRequest : Request<TimeSheetDisplayViewResponse>
    {
        public int UserId { get; set; }

        public GetTimeSheetsCountByUserIdRequest(int userId)
        {
            UserId = userId;
        }
    }
}
