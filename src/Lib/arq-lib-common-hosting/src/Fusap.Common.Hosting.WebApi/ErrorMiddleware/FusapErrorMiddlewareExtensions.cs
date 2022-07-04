using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Fusap.Common.Hosting.WebApi.ErrorMiddleware
{
    public static class FusapErrorMiddlewareExtensions
    {
        private static readonly JsonSerializerSettings JSON_SERIALIZER_SETTINGS = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter()
            }
        };


        public static IApplicationBuilder UseFusapErrorMiddleware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var problemDetails = new
                    {
                        Id = Guid.Parse(System.Diagnostics.Activity.Current.RootId),
                        Code = "InternalError",
                        Message = "Unexpected error",
                        Errors = new[]
                        {
                            new
                            {
                                ErrorCode = "ISE-500",
                                Message = "An internal error has occurred"
                            }
                        }
                    };

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails, JSON_SERIALIZER_SETTINGS));
                });
            });

            return app;
        }
    }
}