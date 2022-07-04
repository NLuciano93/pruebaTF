using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.Projects.V1.Insert;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.Projects.V1
{
    public partial class ProjectsController
    {
        /// <summary>
        /// Add Project Master
        /// </summary>
        /// <param name="input">Input</param>
        /// <response code="201">
        /// Created: indicates that the request has succeeded and has led to the creation of a resource.
        /// </response>
        [HttpPost]
        public async Task<ActionResult<ProjectIdResponse>> AddProjectMasterAsync([FromBody] AddProjectMasterRequest input)
        {
            var result = await Send(input);

            return Created(result);
        }

        /// <summary>
        /// Assign Project to Users
        /// </summary>
        /// <param name="projectId">ProjectId</param>
        /// <param name="input">Input</param>
        [HttpPost("{projectId}/assign-project-to-users")]
        public async Task<ActionResult> AssignProjectToUsersAsync(
            [FromRoute] int projectId,
            [FromBody] AssignProjectToUsersInput input)
        {
            var result = await Send(new AssignProjectToUsersRequest(projectId, input));

            return Ok(result);
        }
    }
}
