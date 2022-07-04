using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Fusap.Common.Hosting.UserAgent
{
    public class UserAgentInjectorRequestHandler : DelegatingHandler
    {
        private readonly FusapHostOptions _options;

        public UserAgentInjectorRequestHandler(IOptions<FusapHostOptions> options)
        {
            _options = options.Value;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.UserAgent.Any())
            {
                var informationalVersion = Assembly
                    .GetEntryAssembly()?
                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                    .InformationalVersion
                    .Replace('/', '-')
                    .Replace(':', '.')
                    ;

                var version = Assembly
                    .GetEntryAssembly()?
                    .GetName()
                    .Version?.ToString();

                request.Headers.TryAddWithoutValidation("User-Agent",
                    $"{_options.ApplicationName}/{informationalVersion ?? version ?? "0.1.0+unknown"}");
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}