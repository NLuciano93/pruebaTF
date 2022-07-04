using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fusap.Common.Swagger
{
    public class ResponsePaginationHeadersFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var paginationAttribute =
                context.ApiDescription.CustomAttributes()
                .OfType<ProducesPaginationResponseHeadersAttribute>()
                .FirstOrDefault();

            if (paginationAttribute == null)
            {
                return;
            }

            if (!operation.Responses.TryGetValue(paginationAttribute.StatusCode.ToString(), out var response))
            {
                return;
            }

            response.Headers ??= new Dictionary<string, OpenApiHeader>();

            response.Headers["X-pageNumber"] = new OpenApiHeader
            {
                AllowEmptyValue = false,
                Schema = new OpenApiSchema
                {
                    Type = HeaderResponseType.Number.ToString().ToLowerInvariant()
                },
                Description = "Current page"
            };
            response.Headers["X-pageSize"] = new OpenApiHeader
            {
                AllowEmptyValue = false,
                Schema = new OpenApiSchema
                {
                    Type = HeaderResponseType.Number.ToString().ToLowerInvariant()
                },
                Description = "Total pages"
            };
            response.Headers["X-totalCount"] = new OpenApiHeader
            {
                AllowEmptyValue = false,
                Schema = new OpenApiSchema
                {
                    Type = HeaderResponseType.Number.ToString().ToLowerInvariant()
                },
                Description = "Total records count"
            };
        }
    }
}