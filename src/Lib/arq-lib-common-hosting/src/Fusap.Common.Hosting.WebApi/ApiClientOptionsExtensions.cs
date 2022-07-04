using System.Net.Http.Headers;
using Fusap.Common.ApiClient.Abstractions;
using Microsoft.AspNetCore.Http;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiClientOptionsExtensions
    {
        public static ApiClientOptions<TInterface> CopyAuthorizationFromCurrentContext<TInterface>(
            this ApiClientOptions<TInterface> options)
        {
            return options.WithAuthenticationHeaderProvider((provider =>
            {
                var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;

                if (httpContext == null)
                {
                    return null;
                }

                var header = (string)httpContext.Request.Headers["Authorization"];

                return AuthenticationHeaderValue.TryParse(header, out var authHeader) ? authHeader : null;
            }));
        }
    }
}
