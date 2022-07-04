using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.Registrations.V1.Update;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.Registrations.V1
{
    public partial class RegistrationsController
    {
        /// <summary>
        /// Update Password
        /// </summary>
        /// <param name="registrationId">RegistrationId</param>
        /// <param name="input">New Password</param>
        /// <returns></returns>
        [HttpPost("{registrationId}/update-password")]
        public async Task<ActionResult> UpdatePasswordAsync([FromRoute] int registrationId, [FromBody] UpdatePasswordInput input)
        {
            var result = await Send(new UpdatePasswordRequest(registrationId, input));

            return Ok(result);
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="registrationId">RegistrationId</param>
        /// <param name="input">New Password</param>
        /// <returns></returns>
        [HttpPost("{registrationId}/change-password")]
        public async Task<ActionResult> ChangePasswordAsync([FromRoute] int registrationId, [FromBody] ChangePasswordInput input)
        {
            var result = await Send(new ChangePasswordRequest(registrationId, input));

            return Ok(result);
        }
    }
}
