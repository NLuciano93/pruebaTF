using System.Data;
using System.Data.Common;
using Fusap.Common.Data;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class FusapDatabaseExtensions
    {
        public static IServiceCollection AddFusapDatabase<TDbConnection>(this IServiceCollection services) where TDbConnection : DbConnection, new()
        {
            // Register the Fusap Connection Factory.
            services.AddSingleton<IFusapDatabase, FusapDatabase<TDbConnection>>();

            services.AddDbConnectionShortcuts();

            return services;
        }

        public static IServiceCollection AddDbConnectionShortcuts(this IServiceCollection services)
        {
            // Register scoped read-write IDbConnection.
            services.AddScoped(provider =>
                provider.GetRequiredService<IFusapDatabase>().CreateConnection());

            // Register scoped shortcut to get the read-only IDbConnection.
            services.AddScoped<IReadOnlyDbConnection>(provider =>
                new ReadOnlyDbConnection((DbConnection)provider.GetRequiredService<IFusapDatabase>().CreateReadOnlyConnection()));

            return services;
        }
    }
}
