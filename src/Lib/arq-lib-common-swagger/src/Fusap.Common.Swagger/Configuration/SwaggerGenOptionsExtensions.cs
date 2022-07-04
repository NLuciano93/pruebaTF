using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fusap.Common.Swagger
{
    public static class SwaggerGenOptionsExtensions
    {
        public static void AddSecurityDefinition(this SwaggerGenOptions options, FusapSwaggerSecurityDefinition fusapSwaggerSecurityDefinition)
        {
            var schemeType = fusapSwaggerSecurityDefinition.Type;

            switch (schemeType)
            {
                case SecuritySchemeType.Http:
                    options.AddSecurityDefinition(schemeType.ToString(), new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "basic"
                    });
                    break;

                case SecuritySchemeType.ApiKey:
                    options.AddSecurityDefinition(schemeType.ToString(), new OpenApiSecurityScheme
                    {
                        Description = fusapSwaggerSecurityDefinition.Description,
                        In = ParameterLocation.Header,
                        Name = fusapSwaggerSecurityDefinition.Name,
                        Type = SecuritySchemeType.ApiKey,
                    });
                    break;

                case SecuritySchemeType.OAuth2:
                    {
                        var openApiOAuthFlow = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(fusapSwaggerSecurityDefinition.AuthorizationUrl),
                            TokenUrl = new Uri(fusapSwaggerSecurityDefinition.TokenUrl),
                        };

                        fusapSwaggerSecurityDefinition.Flow = (fusapSwaggerSecurityDefinition.Flow ?? string.Empty).ToLower();

                        options.AddSecurityDefinition(schemeType.ToString(), new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.OAuth2,
                            Flows = new OpenApiOAuthFlows
                            {
                                Password = fusapSwaggerSecurityDefinition.Flow.Equals("password") ? openApiOAuthFlow : null,
                                ClientCredentials = fusapSwaggerSecurityDefinition.Flow.Equals("clientcredentials")
                                    ? openApiOAuthFlow
                                    : null
                            },
                            Description = fusapSwaggerSecurityDefinition.Description,
                        });
                        break;
                    }

                default:
                    throw new InvalidOperationException($"Unsupported FusapSwagger security definition type: {schemeType}");
            }
        }
    }
}