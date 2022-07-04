using Fusap.TimeSheet.Application.ServiceClient;
using Fusap.TimeSheet.Bootstrap.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fusap.TimeSheet.Bootstrap
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureMVCServices(configuration);
            services.ConfigureMediatrServices();
            services.ConfigurePersistenceServices(configuration);

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.Configure<SwaggerGenOptions>(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
            });

            services.AddTransient<IPolicyService, PolicyService>();

            services.AddFusapPresenter();

            return services;
        }

        public static IApplicationBuilder Configure(
            this IApplicationBuilder app,
            IHostEnvironment hostEnvironment,
            IConfiguration configuration
            )
        {
            app.UsePathBase(configuration["BasePath"]);
            app.ConfigureMVC(hostEnvironment);

            return app;
        }
    }
}
