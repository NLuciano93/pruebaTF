using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Fusap.Common.Swagger
{
    public class FusapSwaggerPostConfigureOptions : IPostConfigureOptions<FusapSwaggerOptions>
    {
        public void PostConfigure(string name, FusapSwaggerOptions options)
        {
            if (options.SecurityDefinitions.Count == 0)
            {
                options.SecurityDefinitions.Add(new FusapSwaggerSecurityDefinition
                {
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "Authorization header (please inform Bearer + your token)"
                });
            }
        }
    }
}