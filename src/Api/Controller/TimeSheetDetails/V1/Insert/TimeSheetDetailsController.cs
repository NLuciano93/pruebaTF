using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.TimeSheetDetails.V1.Insert;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.TimeSheetDetails.V1
{
    public partial class TimeSheetDetailsController
    {
        /// <summary>
        /// Add TimeSheetDetails
        /// </summary>
        /// <param name="input">Input</param>
        /// <response code="201">
        /// Created: indicates that the request has succeeded and has led to the creation of a resource.
        /// </response>
        [HttpPost]
        public async Task<ActionResult<TimeSheetDetailsIdResponse>> AddTimeSheetDetailsAsync([FromBody] AddTimeSheetDetailRequest input)
        {
            var result = await Send(input);

            return Created(result);
        }
    }
}
