using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Fusap.Common.ApiClient.Abstractions
{
    public class ApiClientConfigureOptions<TInterface> : IConfigureOptions<ApiClientOptions<TInterface>>
    {
        private readonly IConfiguration _configuration;

        public ApiClientConfigureOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(ApiClientOptions<TInterface> options)
        {
            options.BaseUrl = _configuration["ConnectionStrings:" + typeof(TInterface).FullName];
        }
    }
}
