using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Fusap.Common.Hosting.HealthChecks
{
    public static class HealthChecksExtensions
    {
        private const string PATH = "/health";

        public static IServiceCollection AddFusapHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddTransient<IStartupFilter, HealthChecksStartupFilter>();

            return services;
        }

        public static IApplicationBuilder UseFusapHealthChecks(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks(PATH, new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });

            return app;
        }
    }
}
