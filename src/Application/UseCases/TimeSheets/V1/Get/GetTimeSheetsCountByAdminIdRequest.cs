using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Get
{
    public class GetTimeSheetsCountByAdminIdRequest : Request<TimeSheetDisplayViewResponse>
    {
        public int AdminId { get; set; }

        public GetTimeSheetsCountByAdminIdRequest(int adminId)
        {
            AdminId = adminId;
        }
    }
}
