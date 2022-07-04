using System;
using Fusap.Common.Hosting.WebApi.ErrorMiddleware;
using Fusap.Common.Hosting.WebApi.Swagger;
using Fusap.Common.Hosting.WebApi.Versioning;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

// This namespace is Microsoft.Extensions.Hosting in order to simplify the usage
// by reducing the number of using statements that frequently pollute the beginning
// of the startup files.
// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting
{
    public static class FusapWebApiBuilderExtensions
    {
        public static IHostBuilder ConfigureFusapWebApi<TStartup>(this IHostBuilder hostBuilder, Action<OpenApiInfo>? configureApiInfo = null) where TStartup : class
        {
            // Configure common pre-startup services
            hostBuilder.ConfigureServices(services =>
            {
                // Add the FusapErrorMiddleware startup filter.
                services.TryAddEnumerable(ServiceDescriptor.Transient<IStartupFilter, FusapErrorMiddlewareStartupFilter>());

                // Add default context accessor
                services.AddHttpContextAccessor();

                // Configure JsonOptions
                services.Configure<JsonOptions>(options => options.JsonSerializerOptions.IgnoreNullValues = true);

                // Add routing
                services.AddRouting(options => options.LowercaseUrls = true);
            });

            // Configure startup
            hostBuilder.ConfigureWebHostDefaults(webHostBuilder =>
            {
                webHostBuilder.UseStartup<TStartup>();
            });

            // Configure post-startup services
            hostBuilder.ConfigureServices(services =>
            {
                // Configure default route prefix
                services.Configure<MvcOptions>(options =>
                {
                    // Add version prefix to all routes if not already present
                    options.Conventions.Add(new VersionPrefixConvention());
                });

                // Add standard swagger configuration
                services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<OpenApiInfo>, OpenApiInfoConfigureOptions>());
                services.AddFusapSwagger(configureApiInfo);
            });

            // Validate host
            hostBuilder.ValidateFusapHost();

            return hostBuilder;
        }
    }
}
