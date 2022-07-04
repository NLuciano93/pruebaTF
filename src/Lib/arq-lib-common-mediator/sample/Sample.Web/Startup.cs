using System;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fusap.Sample.Web.Requests.GetWeatherForecast;

namespace Fusap.Sample.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDistributedMemoryCache();

            var fusapAssemblies = AppDomain.CurrentDomain.GetFusapLoadedAssemblies();

            services.AddFusapMediator(fusapAssemblies, builder =>
            {
                builder.UseAuthorization();
                builder.UseValidation();
            });

            services.AddScoped<IValidator<GetWeatherForecastRequest>, GetWeatherForecastValidator>();
            services.AddScoped<IValidator<GetWeatherForecastRequest>, GetWeatherForecastValidator>();
            services.AddScoped<IValidator<GetWeatherForecastRequest>, GetWeatherForecastValidator>();

            services.AddFusapPresenter();

            //services
            //    .AddMediatorResponseCaching<GetWeatherForecastRequest,
            //        ResponseData<IEnumerable<WeatherForecast>, ResponseMessage>>("test", TimeSpan.FromSeconds(10));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
