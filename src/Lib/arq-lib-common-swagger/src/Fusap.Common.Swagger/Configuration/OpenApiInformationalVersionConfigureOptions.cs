using System;
using System.Reflection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Fusap.Common.Swagger
{
    public class OpenApiInformationalVersionConfigureOptions : IPostConfigureOptions<OpenApiInfo>
    {
        private readonly bool _useVersionFromEntryAssembly;

        public OpenApiInformationalVersionConfigureOptions(IOptions<FusapSwaggerOptions> options)
        {
            _useVersionFromEntryAssembly = options.Value.UseVersionFromEntryAssembly;
        }

        public void PostConfigure(string name, OpenApiInfo options)
        {
            if (!_useVersionFromEntryAssembly)
            {
                options.Version = "1.0.0";
                return;
            }

            var version = Assembly
                .GetEntryAssembly()?
                .GetName()
                .Version?.ToString();

            if (version != null && options.Version == null)
            {
                options.Version = version;
            }

            var informationalVersion = Assembly
                .GetEntryAssembly()?
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                .InformationalVersion;

            if (informationalVersion != null)
            {
                options.Description = (options.Description + Environment.NewLine + Environment.NewLine + "Version: " + informationalVersion + ".").Trim();
            }
        }
    }
}