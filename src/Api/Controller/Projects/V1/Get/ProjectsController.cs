using System.Threading.Tasks;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.UseCases.Projects.V1.Get;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.Projects.V1
{
    public partial class ProjectsController
    {
        /// <summary>
        /// Get Project Master by Id
        /// </summary>
        /// <param name="projectId">ProjectId</param>
        /// <returns></returns>
        /// <response code="404">
        /// Not Found: indicates that the server can't find the requested resource.
        /// </response>
        [HttpGet("{projectId}")]
        public async Task<ActionResult<ProjectMasterResponse>> GetProjectMasterByIdAsync([FromRoute] int projectId)
        {
            var result = await Send(new GetProjectMasterByIdRequest(projectId));

            return OkAs<ProjectMasterResponse>(result);
        }

        /// <summary>
        /// Show all Projects
        /// </summary>
        /// <param name="input">Request for query input</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Pagination<ProjectMasterResponse>>> ShowAllProjectsAsync([FromQuery] ShowAllProjectsInput input)
        {
            var request = Map<ShowAllProjectsRequest>(input);

            var result = await Send(request);

            return OkAs<Pagination<ProjectMasterResponse>>(result);
        }

        /// <summary>
        /// Get List of Proyects
        /// </summary>
        /// <returns></returns>
        [HttpGet("list-of-projects")]
        public async Task<ActionResult<ProjectListResponse>> GetListOfProyectsAsync()
        {
            var result = await Send(new GetListOfProjectsRequest());

            return Ok(result);
        }

        /// <summary>
        /// Get list of Projects by UserAdmin
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <returns></returns>
        [HttpGet("list-of-projects-by-user-admin")]
        public async Task<ActionResult<ProjectListResponse>> GetListofProjectsByUserAdmin([FromQuery] int userId)
        {
            var result = await Send(new GetListofProjectsByUserAdminRequest(userId));

            return OkAs<ProjectListResponse>(result);
        }

        /// <summary>
        /// Get list of Projects by User
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <returns></returns>
        [HttpGet("list-of-projects-by-user")]
        public async Task<ActionResult<ProjectListResponse>> GetListofProjectsByUser([FromQuery] int userId)
        {
            var result = await Send(new GetListofProjectsByUserRequest(userId));

            return OkAs<ProjectListResponse>(result);
        }

        /// <summary>
        /// Check is projectCode exists
        /// </summary>
        /// <param name="projectCode">ProjectCode</param>
        /// <returns></returns>
        /// <response code="404">
        /// Not Found: indicates that the server can't find the requested resource.
        /// </response>
        [HttpGet("check-project-code-exist")]
        public async Task<ActionResult> CheckProjectCodeExistsAsync([FromQuery] string projectCode)
        {
            var result = await Send(new CheckProjectCodeExistsRequest(projectCode));

            return Ok(result);
        }

        /// <summary>
        /// Check is projectName exists
        /// </summary>
        /// <param name="projectName">ProjectName</param>
        /// <returns></returns>
        /// <response code="404">
        /// Not Found: indicates that the server can't find the requested resource.
        /// </response>
        [HttpGet("check-project-name-exist")]
        public async Task<ActionResult> CheckProjectNameExistsAsync([FromQuery] string projectName)
        {
            var result = await Send(new CheckProjectNameExistsRequest(projectName));

            return Ok(result);
        }

        /// <summary>
        /// Get total projects counts
        /// </summary>
        /// <returns></returns>
        [HttpGet("total-projects-count")]
        public async Task<ActionResult<int>> GetTotalProjectsCountsAsync()
        {
            var result = await Send(new GetTotalProjectsCountsRequest());

            return OkAs<int>(result);
        }

        /// <summary>
        /// Get project name by Id
        /// </summary>
        /// <param name="projectId">ProjectId</param>
        /// <returns></returns>
        [HttpGet("project-name-by-id")]
        public async Task<ActionResult<string>> GetProjectNameByIdAsync([FromQuery] int projectId)
        {
            var result = await Send(new GetProjectNameByIdRequest(projectId));

            return OkAs<string>(result);
        }
    }
}
