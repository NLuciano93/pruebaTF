using Microsoft.AspNetCore.Hosting;
using Fusap.Common.Hosting.Worker;

// This namespace is Microsoft.Extensions.Hosting in order to simplify the usage
// by reducing the number of using statements that frequently pollute the beginning
// of the startup files.
// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting
{
    public static class FusapWorkerBuilderExtensions
    {
        public static IHostBuilder ConfigureFusapWorker(this IHostBuilder hostBuilder)
        {
            // Configure empty startup
            hostBuilder.ConfigureWebHostDefaults(configure =>
            {
                configure.UseStartup<EmptyStartup>();
            });

            // Validate host
            hostBuilder.ValidateFusapHost();

            return hostBuilder;
        }
    }
}