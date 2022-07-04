using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.Roles.V1.Insert;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.Roles.V1
{
    public partial class RolesController
    {
        /// <summary>
        /// Assign Roles
        /// </summary>
        /// <param name="input">Input</param>
        [HttpPost]
        public async Task<ActionResult> AssignRolesAsync([FromBody] AssignRolesRequest input)
        {
            await Send(input);

            return Ok();
        }
    }
}
