using System;
using System.Linq;
using System.Text.Json.Serialization;
using Fusap.Common.Model;
using Fusap.Common.Model.Presenter.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class WebApiPresenterServiceCollectionExtensions
    {
        public static IServiceCollection AddFusapPresenter(this IServiceCollection services)
        {
            services.ConfigureOptions<PresenterConfigureDefaultOptions>();
            services.TryAddSingleton<IPresenter, Presenter>();

            services.AddSingleton<IClientErrorFactory, ClientModelErrorFactory>();
            services.AddSingleton<IStartupFilter, PresenterStartupFilter>();

            services.Configure<MvcOptions>(opt =>
            {
#pragma warning disable S109 // Magic numbers should not be used
                opt.Filters.Add<PresentErrorFilter>(-6000);
                opt.Filters.Add<PresentResultFilter>(-6000);
                opt.Filters.Add<PresentExceptionFilter>(-7000);
#pragma warning restore S109 // Magic numbers should not be used
            });

            services.Configure<JsonOptions>(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opts.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.PostConfigure<ApiBehaviorOptions>(options =>
            {
                options.ClientErrorMapping[StatusCodes.Status422UnprocessableEntity] = new ClientErrorData
                {
                    Link = "https://tools.ietf.org/html/rfc4918#section-11.2",
                    Title = "Business rules violated",
                };

                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var presenter = actionContext.HttpContext.RequestServices.GetRequiredService<IPresenter>();

                    var details = actionContext
                        .ModelState
                        .SelectMany(kv => kv.Value.Errors.Select(error =>
                            new ValidationErrorDetail(kv.Key, "INVALID", error.ErrorMessage)))
                        .ToArray();

                    return presenter.Present(new ValidationError(details));
                };
            });

            return services;
        }

        public static IServiceCollection AddFusapPresenter(this IServiceCollection services,
            Action<PresenterOptions> configureOptions)
        {
            services.AddFusapPresenter();
            services.Configure(configureOptions);

            return services;
        }
    }
}
