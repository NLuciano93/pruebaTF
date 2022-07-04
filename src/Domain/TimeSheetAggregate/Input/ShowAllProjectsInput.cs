namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Input
{
    public class ShowAllProjectsInput
    {
        /// <summary>
        /// Project Code
        /// </summary>
        public string? ProjectCode { get; set; }

        /// <summary>
        /// Project Name
        /// </summary>
        public string? ProjectName { get; set; }

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
