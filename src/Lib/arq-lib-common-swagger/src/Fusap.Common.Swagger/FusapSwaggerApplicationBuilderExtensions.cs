using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

// This namespace is Microsoft.AspNetCore.Builder in order to simplify the usage
// by reducing the number of using statements that frequently pollute the beginning
// of the startup files.
// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class FusapSwaggerApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseFusapSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }

        public static IApplicationBuilder UseFusapSwaggerHomeRedirect(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                var uiOptions = endpoints.ServiceProvider.GetService<IOptions<SwaggerUIOptions>>().Value;

                endpoints.MapFallback("/", context =>
                {
                    context.Response.Redirect($"{context.Request.PathBase.Value}/{uiOptions.RoutePrefix}");
                    return Task.CompletedTask;
                });
            });

            return app;
        }
    }
}
