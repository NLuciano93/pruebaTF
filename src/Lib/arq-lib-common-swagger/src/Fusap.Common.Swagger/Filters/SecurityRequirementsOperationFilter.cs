using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fusap.Common.Swagger
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // If the method allow anonymous, there is no security.
            if (context.MethodInfo.GetCustomAttribute<AllowAnonymousAttribute>(true) != null)
            {
                return;
            }

            // If there is no AuthorizeAttribute anywhere, there is also no security.
            var authorizeAttribute =
                context.MethodInfo.GetCustomAttribute<AuthorizeAttribute>(true) ??
                context.MethodInfo.DeclaringType?.GetCustomAttribute<AuthorizeAttribute>(true);
            if (authorizeAttribute == null)
            {
                return;
            }

            // Extract the SecuritySchemeAttribute.
            var securitySchemeAttribute =
                context.MethodInfo.GetCustomAttribute<SecuritySchemeAttribute>(true) ??
                context.MethodInfo.DeclaringType?.GetCustomAttribute<SecuritySchemeAttribute>(true);

            var schemeId = securitySchemeAttribute?.Value ?? SecuritySchemeType.ApiKey;

            // Create scheme defaulting to OAuth2.
            var scheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = schemeId.ToString()
                }
            };

            operation.Security = new List<OpenApiSecurityRequirement> {
                new OpenApiSecurityRequirement
                {
                    { scheme, Array.Empty<string>() }
                }
            };
        }
    }
}
