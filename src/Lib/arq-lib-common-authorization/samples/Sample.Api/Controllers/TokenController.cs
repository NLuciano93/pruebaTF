using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Fusap.Common.Authorization.WebApi;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("JwtIssuer");
        }

        [HttpGet]
        [Route("{personId}")]
        public IActionResult GenerateToken([FromRoute] Guid personId)
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
                new Claim(JwtRegisteredClaimNames.Sub, personId.ToString("N")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.Iat, nowUtc.ToString())
            };

            var jwtPayload = new JwtPayload(iss, aud, claims, nowUtc, expires);

            var jwt = new JwtSecurityToken(jwtHeader, jwtPayload);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwt);

            return Ok(new
            {
                Token = token,
                Expires = expires
            });
        }
    }
}
