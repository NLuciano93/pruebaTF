using System;
using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.Expenses.V1.Insert
{
    public class AddExpenseRequest : Request<ExpenseIdResponse>
    {
        public string PurposeorReason { get; set; } = default!;
        public int? ExpenseStatus { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? VoucherId { get; set; }
        public int? HotelBills { get; set; }
        public int? TravelBills { get; set; }
        public int? MealsBills { get; set; }
        public int? LandLineBills { get; set; }
        public int? TransportBills { get; set; }
        public int? MobileBills { get; set; }
        public int? Miscellaneous { get; set; }
        public int? TotalAmount { get; set; }
        public int? UserId { get; set; }
        public string? Comment { get; set; }
        public int? ProjectId { get; set; }
    }
}
