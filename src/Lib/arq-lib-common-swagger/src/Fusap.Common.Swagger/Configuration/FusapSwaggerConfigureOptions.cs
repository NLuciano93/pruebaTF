using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Fusap.Common.Swagger
{
    public class FusapSwaggerConfigureOptions : IConfigureOptions<FusapSwaggerOptions>
    {
        private readonly IConfiguration _configuration;

        public FusapSwaggerConfigureOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(FusapSwaggerOptions options)
        {
            // SwaggerSettings key for backwards configuration compatibility.
            _configuration.Bind("SwaggerSettings", options);

            // This is the default configuration key going forward.
            _configuration.Bind("FusapSwagger", options);
        }
    }
}