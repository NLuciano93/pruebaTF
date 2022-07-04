namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Input
{
    public class ShowNotificationsTbInput
    {
        /// <summary>
        /// Message
        /// </summary>
        public string? Message { get; set; }

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
