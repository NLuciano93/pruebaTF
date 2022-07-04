using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.Registrations.V1.Get;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.Registrations.V1
{
    public partial class RegistrationsController
    {
        /// <summary>
        /// Get RegistrationId by Registration Username
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns></returns>
        /// <response code="404">
        /// Not Found: indicates that the server can't find the requested resource.
        /// </response>
        [HttpGet("registrationid-by-username")]
        public async Task<ActionResult<int>> GetRegistrationIdByUsernameAsync([FromQuery] string userName)
        {
            var result = await Send(new GetRegistrationIdByUsernameRequest(userName));

            return OkAs<int>(result);
        }
    }
}
