using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fusap.Common.Swagger
{
    public class LowercaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths.Extensions = swaggerDoc.Paths.Extensions
                .ToDictionary(keyValuePair => LowercaseEverythingButParameters(keyValuePair.Key), keyValuePair => keyValuePair.Value);
        }

        private static string LowercaseEverythingButParameters(string key)
        {
            return string.Join("/", key.Split('/').Select(x => x.Contains("{") ? x : x.ToLowerInvariant()));
        }
    }
}
