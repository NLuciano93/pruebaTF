using Microsoft.OpenApi.Models;

namespace Fusap.Common.Swagger
{
    public class FusapSwaggerSecurityDefinition
    {
        public SecuritySchemeType Type { get; set; }
        public string Flow { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string TokenUrl { get; set; } = default!;
        public string AuthorizationUrl { get; set; } = default!;
        public string In { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
