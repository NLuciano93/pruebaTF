// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;

namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Entities
{
    public partial class Documents
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public byte[] DocumentBytes { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ExpenseId { get; set; }
        public string DocumentType { get; set; }
    }
}
