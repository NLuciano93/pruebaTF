using System;
using System.Security.Cryptography;
using Fusap.Common.Authorization.WebApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// This namespace is Microsoft.Extensions.DependencyInjection in order to simplify the usage
// by reducing the number of using statements that frequently pollute the beginning
// of the startup files.
// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class FusapAuthorizationExtensions
    {
        public static IServiceCollection AddFusapAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt => opt.BindWithFusapOptions(configuration.GetSection("Jwt")));

            return services;
        }

        public static void BindWithFusapOptions(this JwtBearerOptions options, IConfigurationSection configurationSection)
        {
            options.IncludeErrorDetails = configurationSection.GetValue<bool>("IncludeErrorDetails");
            options.RequireHttpsMetadata = configurationSection.GetValue<bool>("RequireHttpsMetadata");
            options.SaveToken = true;

            var rsaParameters = configurationSection.GetSection("PublicKeyValue").Get<RSAStringParameters>();

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configurationSection["Issuer"],
                ValidAudience = configurationSection["Audience"],
                IssuerSigningKey = new RsaSecurityKey(RSA.Create(rsaParameters)),
                RequireSignedTokens = true,
                ValidateIssuer = configurationSection.GetValue<bool>("ValidateIssuer"),
                ValidateAudience = configurationSection.GetValue<bool>("ValidateAudience"),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMilliseconds(configurationSection.GetValue<int>("ClockSkew"))
            };
        }
    }
}
