using System;
using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheetAuditTb.Insert
{
    public class InsertTimeSheetAuditLogRequest : Request<InsertTimeSheetAuditLogIdResponse>
    {
        public int? ApprovalUser { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? Comment { get; set; }
        public int? Status { get; set; }
        public int? TimeSheetId { get; set; }
        public int? UserId { get; set; }
    }
}
