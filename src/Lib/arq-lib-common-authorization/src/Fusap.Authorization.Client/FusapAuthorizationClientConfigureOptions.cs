using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Fusap.Common.Authorization.Client
{
    [ExcludeFromCodeCoverage]
    public class FusapAuthorizationClientConfigureOptions : IConfigureOptions<FusapAuthorizationClientOptions>
    {
        private readonly IConfiguration _configuration;

        public FusapAuthorizationClientConfigureOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(FusapAuthorizationClientOptions options)
        {
            var section = _configuration.GetSection("FusapAuthorizationClient");

            section.Bind(options);
        }
    }
}