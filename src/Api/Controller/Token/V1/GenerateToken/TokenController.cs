using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Fusap.Common.Authorization.WebApi;
using Fusap.TimeSheet.Application.UseCases.Token.V1.GenerateToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Fusap.TimeSheet.Api.Controller.Token.V1
{
    public partial class TokenController
    {
        /// <summary>
        /// Generate Token
        /// </summary>
        /// <param name="login">Username Password</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> GenerateTokenAsync([FromBody] GenerateTokenRequest login)
        {
            var userId = await _mediator.Send(login);

            if (userId?.Value != null && userId.Error == null)
            {
                return Ok(GenerateToken((int)userId.Value, login.Username));
            }

            return Unauthorized(new object());
        }

        private LoginResponse GenerateToken(int userId, string userName)
        {
            var rsaParameters = _configuration.GetSection("PrivateKeyValue").Get<RSAStringParameters>();
            var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsaParameters), SecurityAlgorithms.RsaSha256);

            var jwtHeader = new JwtHeader(signingCredentials);

            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(Convert.ToDouble(_configuration["ExpiresMinutes"]));
            var iss = _configuration["Issuer"];
            var aud = _configuration["Audience"];

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sid, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.Iat, nowUtc.ToString())
            };

            var jwtPayload = new JwtPayload(iss, aud, claims, nowUtc, expires);

            var jwt = new JwtSecurityToken(jwtHeader, jwtPayload);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwt);

            return new LoginResponse()
            {
                Token = token,
                Expires = expires
            };
        }
    }
}
