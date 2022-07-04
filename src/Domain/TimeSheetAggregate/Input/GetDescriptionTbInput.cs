namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Input
{
    public class GetDescriptionTbInput
    {
        /// <summary>
        /// ProjectId
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// TimeSheetMasterId
        /// </summary>
        public int TimeSheetMasterId { get; set; }
    }
}
