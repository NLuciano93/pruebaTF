using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.DescriptionTb.V1.Get;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.DescriptionTb.V1
{
    public partial class DescriptionTbController
    {
        /// <summary>
        /// Get Description Tb by ProyectId and TimeSheetMasterId
        /// </summary>
        /// <param name="input">Request for query input</param>
        /// <returns></returns>
        /// <response code="200">
        /// Ok: indicates that the request has succeeded
        /// </response>
        /// <response code="404">
        /// Not Found: indicates that the server can't find the requested resource.
        /// </response>
        [HttpGet]
        public async Task<ActionResult<DescriptionTbResponse>> GetDescriptionTbAsync([FromQuery] GetDescriptionTbInput input)
        {
            var request = Map<GetDescriptionTbRequest>(input);

            var result = await Send(request);

            return OkAs<DescriptionTbResponse>(result);
        }
    }
}
