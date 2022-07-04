using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fusap.Common.Swagger
{
    public class ResponseHeadersFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Get all response header declarations for a given operation
            var actionResponsesWithHeaders = context.ApiDescription.CustomAttributes()
                .OfType<ProducesResponseHeaderAttribute>()
                .ToArray();

            if (!actionResponsesWithHeaders.Any())
            {
                return;
            }

            foreach (var responseCode in operation.Responses.Keys)
            {
                // Do we have one or more headers for the specific response code
                var responseHeaders = actionResponsesWithHeaders
                    .Where(resp => resp.StatusCode.ToString() == responseCode);
                if (!responseHeaders.Any())
                {
                    continue;
                }

                var response = operation.Responses[responseCode];

                response.Headers ??= new Dictionary<string, OpenApiHeader>();

                foreach (var responseHeader in responseHeaders)
                {
                    response.Headers[responseHeader.Name] = new OpenApiHeader
                    {
                        AllowEmptyValue = false,
                        Schema = new OpenApiSchema
                        {
                            Type = responseHeader.Type.ToString().ToLowerInvariant()
                        },
                        Description = responseHeader.Description
                    };
                }
            }
        }
    }
}