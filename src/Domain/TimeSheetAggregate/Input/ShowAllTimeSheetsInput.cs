namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Input
{
    public class ShowAllTimeSheetsInput
    {
        /// <summary>
        /// UserId
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// TimeSheetStatus
        /// </summary>
        public int? TimeSheetStatus { get; set; }

        /// <summary>
        /// Sort Column
        /// </summary>
        public string? SortColumn { get; set; }

        /// <summary>
        /// Sort Direction
        /// </summary>
        public string? SortDirection { get; set; }

        /// <summary>
        /// Offset
        /// </summary>
        public int Offset { get; set; }
    }
}
