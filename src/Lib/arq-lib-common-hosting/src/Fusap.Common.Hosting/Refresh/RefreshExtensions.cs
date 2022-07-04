using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Steeltoe.Management.Endpoint.Refresh;

namespace Fusap.Common.Hosting.Refresh
{
    public static class RefreshExtensions
    {
        public static IHostBuilder AddFusapRefreshActuator(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                .ConfigureServices((context, collection) =>
                {
                    collection.AddRefreshActuator(context.Configuration);
                    collection.AddTransient<IStartupFilter, RefreshLastStartupFilter>();
                });
        }

    }
}