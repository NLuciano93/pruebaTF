using Fusap.Common.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Fusap.TimeSheet.Api.Controller.Users.V1
{
    /// <summary>
    /// </summary>
    /// <response code="400">
    /// Bad Request: indicates that the server cannot or will not process the request due to something that is perceived to be a client error.
    /// </response>
    /// <response code="401">
    /// Unauthorized: indicates that the requested resource requires authentication.
    /// </response>
    /// <response code="403">
    /// Forbidden: indicates that the server refuses to fulfill the request.
    /// </response>
    /// <response code="422">
    /// Unprocessable Entity: indicates that the request was well-formed but was unable to be followed due to semantic errors.
    /// </response>
    [Authorize]
    [SecurityScheme(SecuritySchemeType.ApiKey)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiVersion("1")]
    public partial class UsersController : TimeSheetController
    {
        public UsersController()
        {
        }
    }
}
