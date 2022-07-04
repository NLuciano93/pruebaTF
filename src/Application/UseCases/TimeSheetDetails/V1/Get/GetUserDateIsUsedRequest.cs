using System;
using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheetDetails.V1.Get
{
    public class GetUserDateIsUsedRequest : Request<UserDateIsUsedResponse>
    {
        public DateTime Period { get; set; }
        public int UserId { get; set; }
    }
}
