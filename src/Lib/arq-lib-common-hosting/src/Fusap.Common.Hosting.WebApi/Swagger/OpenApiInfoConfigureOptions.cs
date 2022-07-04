using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Fusap.Common.Hosting.WebApi.Swagger
{
    public class OpenApiInfoConfigureOptions : IConfigureOptions<OpenApiInfo>
    {
        private readonly FusapHostOptions _fusapHostOptions;

        public OpenApiInfoConfigureOptions(IOptions<FusapHostOptions> fusapHostOptions)
        {
            _fusapHostOptions = fusapHostOptions.Value;
        }

        public void Configure(OpenApiInfo options)
        {
            options.Title = $"Fusapdigital {_fusapHostOptions.ApplicationName} Api";
        }
    }
}
