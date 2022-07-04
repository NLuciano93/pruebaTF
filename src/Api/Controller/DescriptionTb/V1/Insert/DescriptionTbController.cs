using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.DescriptionTb.V1.Insert;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.DescriptionTb.V1
{
    public partial class DescriptionTbController
    {
        /// <summary>
        /// Add DescriptionTb
        /// </summary>
        /// <param name="input">Input</param>
        /// <response code="201">
        /// Created: indicates that the request has succeeded and has led to the creation of a resource.
        /// </response>
        [HttpPost]
        public async Task<ActionResult<DescriptionTbIdResponse>> AddDescriptionTbAsync([FromBody] AddDescriptionTbRequest input)
        {
            var result = await Send(input);

            return Created(result);
        }
    }
}
