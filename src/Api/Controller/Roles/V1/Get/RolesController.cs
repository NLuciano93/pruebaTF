using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.Roles.V1.Get;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.Roles.V1
{
    public partial class RolesController
    {
        /// <summary>
        /// Get Assigned Roles
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<AssignedRolesResponse>> GetAssignedRolesAsync()
        {
            var result = await Send(new GetAssignedRolesRequest());

            return Ok(result);
        }

        /// <summary>
        /// Get RoleId by Rolename
        /// </summary>
        /// <param name="roleName">RoleName</param>
        /// <returns></returns>
        /// <response code="404">
        /// Not Found: indicates that the server can't find the requested resource.
        /// </response>
        [HttpGet("roleid-by-rolename")]
        public async Task<ActionResult<int>> GetRoleIdbyRolenameAsync([FromQuery] string roleName)
        {
            var result = await Send(new GetRoleIdbyRolenameRequest(roleName));

            return OkAs<int>(result);
        }
    }
}
