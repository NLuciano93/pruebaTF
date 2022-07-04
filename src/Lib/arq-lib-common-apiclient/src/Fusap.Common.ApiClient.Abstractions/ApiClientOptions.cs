using System;
using System.Net.Http.Headers;

namespace Fusap.Common.ApiClient.Abstractions
{
    public class ApiClientOptions<TInterface>
    {
        public string BaseUrl { get; set; } = default!;

        public Func<IServiceProvider, AuthenticationHeaderValue?>? AuthenticationHeaderProvider { get; set; }
    }
}
