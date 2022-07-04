namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Response
{
    public class ExpenseDisplayViewResponse
    {
        public int ApprovalUser { get; set; }
        public int SubmittedCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
    }
}
