using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Fusap.Common.Swagger;
using Fusap.TimeSheet.Application.UseCases.Token.V1.GetById;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Fusap.TimeSheet.Api.Controller.Token.V1
{
    public partial class TokenController
    {
        /// <summary>
        /// Get user detail
        /// </summary>
        /// <returns></returns>
        /// <response code="404">
        /// Not Found: indicates that the server can't find the requested resource.
        /// </response>
        [Authorize]
        [SecurityScheme(SecuritySchemeType.ApiKey)]
        [HttpGet]
        public async Task<ActionResult<UserDetailResponse>> GetAsync()
        {
            var token = TokenLibrary.GetToken(Request.Headers["Authorization"]);
            var jwtToken = new JwtSecurityToken(token);
            var result = await _mediator.Send(new GetUserRequest() { Username = jwtToken.Subject });

            return Ok(result);
        }
    }
}
