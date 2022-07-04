using System;
using Fusap.Common.Swagger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

// This namespace is Microsoft.AspNetCore.Builder in order to simplify the usage
// by reducing the number of using statements that frequently pollute the beginning
// of the startup files.
// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class FusapSwaggerCollectionExtensions
    {
        public static IServiceCollection AddFusapSwagger(this IServiceCollection services, Action<OpenApiInfo>? apiInfo = null)
        {
            // Add configurators for all swagger options.
            services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<SwaggerOptions>, SwaggerConfigureOptions>());
            services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<SwaggerGenOptions>, SwaggerGenConfigureOptions>());
            services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<SwaggerUIOptions>, SwaggerUIConfigureOptions>());
            services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<FusapSwaggerOptions>, FusapSwaggerConfigureOptions>());
            services.TryAddEnumerable(ServiceDescriptor.Transient<IPostConfigureOptions<FusapSwaggerOptions>, FusapSwaggerPostConfigureOptions>());

            // Add configurator for OpenApiInfo.
            if (apiInfo != null)
            {
                services.Configure(apiInfo);
            }

            // Add SwaggerGen.
            services.AddSwaggerGen();

            // Add default versioning support.
            services.AddFusapApiVersioning();
            services.ConfigureOptions<OpenApiInformationalVersionConfigureOptions>();

            // Add default grouping.
            services.TryAddSingleton<IGroupingProvider, ApiExposureGroupingProvider>();

            // Add startup filter to setup app
            services.TryAddEnumerable(ServiceDescriptor.Transient<IStartupFilter, SwaggerStartupFilter>());

            // Add default validation
            services.Configure<SwaggerDocumentValidationOptions>(opt => { });

            return services;
        }

        public static IServiceCollection ConfigureApiInfo(this IServiceCollection services, Action<OpenApiInfo> apiInfo)
        {
            services.Configure(apiInfo);

            return services;
        }

        public static IServiceCollection AddFusapApiVersioning(this IServiceCollection services)
        {
            services.AddTransient<IApiVersionDescriptionProvider, DefaultApiVersionDescriptionProvider>();

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            return services;
        }
    }
}
