using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fusap.Common.Swagger
{
    public class MultipartFormDataExclusiveOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var requestBodyContent = operation
                .RequestBody?.Content.Keys;

            if (requestBodyContent == null)
            {
                return;
            }

            if (requestBodyContent.Count <= 1 ||
                !requestBodyContent.Contains("multipart/form-data"))
            {
                return;
            }

            var keysToRemove = requestBodyContent
                .Where(x => x != "multipart/form-data")
                .ToArray();

            foreach (var key in keysToRemove)
            {
                operation.RequestBody?.Content.Remove(key);
            }
        }
    }
}
