using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Fusap.Common.Swagger
{
    public class SwaggerConfigureOptions : IConfigureOptions<SwaggerOptions>
    {
        public void Configure(SwaggerOptions options)
        {
            options.PreSerializeFilters.Add((apiDocument, request) =>
            {
                apiDocument.Servers = new List<OpenApiServer>
                {
                    new OpenApiServer { Url = $"{GetScheme(request)}://{request.Host.Value}{request.PathBase}" }
                };
            });
        }

        private static string GetScheme(HttpRequest request)
        {
            if (!request.Host.HasValue)
            {
                return "https";
            }

            var displayUri = new Uri(request.GetDisplayUrl());

            return displayUri.IsLoopback ? displayUri.Scheme : "https";
        }
    }
}