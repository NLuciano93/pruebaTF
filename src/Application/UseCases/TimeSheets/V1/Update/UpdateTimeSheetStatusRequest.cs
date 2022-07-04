using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Update
{
    public class UpdateTimeSheetStatusRequest : Request
    {
        public int TimeSheetMasterId { get; set; }
        public int TimeSheetStatus { get; set; }
        public string? Comment { get; set; }

        public UpdateTimeSheetStatusRequest(int timeSheetMasterId, UpdateTimeSheetStatusInput input)
        {
            TimeSheetMasterId = timeSheetMasterId;
            TimeSheetStatus = input.TimeSheetStatus;
            Comment = input.Comment;
        }
    }
}
