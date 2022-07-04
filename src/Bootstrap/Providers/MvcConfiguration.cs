using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fusap.TimeSheet.Bootstrap.Providers
{
    public static class MvcConfiguration
    {
        public static IServiceCollection ConfigureMVCServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            services.AddFusapAuthentication(configuration);
            services.AddAuthorization();

            return services;
        }

        public static IApplicationBuilder ConfigureMVC(this IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("health");
            });

            return app;
        }
    }
}
