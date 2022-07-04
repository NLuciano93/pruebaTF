using Fusap.Common.Authorization.Client;

// This namespace is Microsoft.Extensions.DependencyInjection in order to simplify the usage
// by reducing the number of using statements that frequently pollute the beginning
// of the startup files.
// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class FusapAuthorizationClientServiceCollectionExtensions
    {
        public static IServiceCollection AddFusapAuthorizationClient(this IServiceCollection services)
        {
            services.AddHttpClient<IFusapAuthorizationClient, FusapAuthorizationClient>();

            services.ConfigureOptions<FusapAuthorizationClientConfigureOptions>();

            return services;
        }
    }
}
