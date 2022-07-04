using System;

namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject
{
    public class UsersHoursNoCompleteDto
    {
        public DateTime? Date { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public int Offset { get; set; }
    }
}
