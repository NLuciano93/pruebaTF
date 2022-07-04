using Fusap.Common.Mediator;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Delete
{
    public class DeleteTimeSheetMasterRequest : Request
    {
        public DeleteTimeSheetMasterRequest(int timeSheetMasterId, int userId)
        {
            UserId = userId;
            TimeSheetMasterId = timeSheetMasterId;
        }

        public int TimeSheetMasterId { get; set; }
        public int UserId { get; set; }
    }
}
