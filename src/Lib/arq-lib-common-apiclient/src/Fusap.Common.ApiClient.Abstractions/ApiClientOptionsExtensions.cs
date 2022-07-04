using System;
using System.Net.Http.Headers;

namespace Fusap.Common.ApiClient.Abstractions
{
    public static class ApiClientOptionsExtensions
    {
        public static ApiClientOptions<TInterface> WithAuthenticationHeaderProvider<TInterface>(this ApiClientOptions<TInterface> options,
            Func<IServiceProvider, AuthenticationHeaderValue?> authenticationHeaderProvider)
        {
            options.AuthenticationHeaderProvider = authenticationHeaderProvider;

            return options;
        }

        public static ApiClientOptions<TInterface> WithBaseUrl<TInterface>(this ApiClientOptions<TInterface> options,
            string baseUrl)
        {
            options.BaseUrl = baseUrl;

            return options;
        }
    }
}
