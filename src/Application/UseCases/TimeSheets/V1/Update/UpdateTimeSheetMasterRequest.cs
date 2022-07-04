using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Update
{
    public class UpdateTimeSheetMasterRequest : Request
    {
        public int TimeSheetMasterId { get; set; }
        public decimal TotalHours { get; set; }

        public UpdateTimeSheetMasterRequest(int timeSheetMasterId, UpdateTimeSheetMasterInput input)
        {
            TimeSheetMasterId = timeSheetMasterId;
            TotalHours = input.TotalHours;
        }
    }
}
