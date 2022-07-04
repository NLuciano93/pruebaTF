using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Fusap.Common.ApiClient.Abstractions
{
    public static class ApiClientServiceExtensions
    {
        public static IHttpClientBuilder AddApiClient<TInterface, TImplementation>
            (this IServiceCollection services)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            var builder = services.AddHttpClient<TInterface, TImplementation>()
                .ConfigureHttpClient((provider, client) =>
                {
                    var options = provider.GetRequiredService<IOptions<ApiClientOptions<TInterface>>>().Value;

                    if (string.IsNullOrWhiteSpace(options.BaseUrl))
                    {
#pragma warning disable S3928 // Parameter names used into ArgumentException constructors should match an existing one
                        throw new ArgumentNullException(nameof(options.BaseUrl),
                            "No url was provided for Api Client " + typeof(TInterface).FullName);
#pragma warning restore S3928 // Parameter names used into ArgumentException constructors should match an existing one
                    }

                    client.BaseAddress = new Uri(options.BaseUrl);

                    var authHeader = options.AuthenticationHeaderProvider?.Invoke(provider);
                    if (authHeader != null)
                    {
                        client.DefaultRequestHeaders.Authorization = authHeader;
                    }
                });

            services.ConfigureOptions<ApiClientConfigureOptions<TInterface>>();

            return builder;
        }

        public static IHttpClientBuilder AddApiClient<TInterface, TImplementation>
            (this IServiceCollection services, Action<ApiClientOptions<TInterface>> configureOptions)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            var builder = services.AddApiClient<TInterface, TImplementation>();

            services.Configure(configureOptions);

            return builder;
        }
    }
}
