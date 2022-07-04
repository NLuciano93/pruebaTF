using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Fusap.Common.Model.Presenter.WebApi;
using Fusap.TimeSheet.Api.Controller.Token;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class TimeSheetController : FusapApiController
    {
        public int GetUser()
        {
            var token = TokenLibrary.GetToken(Request.Headers["Authorization"]);
            var jwtToken = new JwtSecurityToken(token);
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "sid");

            return int.Parse(userId.Value);
        }
    }
}
