using System.Threading.Tasks;
using Fusap.TimeSheet.Application.UseCases.Projects.V1.Delete;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.Projects.V1
{
    public partial class ProjectsController
    {
        /// <summary>
        /// Delete Project Master
        /// </summary>
        /// <param name="projectId">ProjectId</param>
        /// <returns></returns>
        [HttpDelete("{projectId}")]
        public async Task<ActionResult> DeleteProjectMasterAsync([FromRoute] int projectId)
        {
            var result = await Send(new DeleteProjectMasterRequest(projectId));

            return Ok(result);
        }
    }
}
