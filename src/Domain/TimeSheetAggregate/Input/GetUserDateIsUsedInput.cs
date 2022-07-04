using System;

namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Input
{
    public class GetUserDateIsUsedInput
    {
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Period { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }
    }
}
