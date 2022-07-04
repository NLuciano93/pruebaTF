using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.Registrations.V1.Insert;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.Registrations.V1
{
    public partial class RegistrationsController
    {
        /// <summary>
        /// Add Registration
        /// </summary>
        /// <param name="input">Input</param>
        /// <response code="201">
        /// Created: indicates that the request has succeeded and has led to the creation of a resource.
        /// </response>
        [HttpPost]
        public async Task<ActionResult<RegistrationIdResponse>> AddRegistrationAsync([FromBody] AddRegistrationRequest input)
        {
            var result = await Send(input);

            return Created(result);
        }
    }
}
