using Fusap.TimeSheet.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fusap.TimeSheet.Bootstrap.Providers
{
    public static class PersistenceConfiguration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFusapSqlServer();
            services.AddScoped<IService, DbConnectionConsumerService>();

            return services;
        }
    }
}
