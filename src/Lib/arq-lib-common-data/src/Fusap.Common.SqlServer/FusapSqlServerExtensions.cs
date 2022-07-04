using System.Data.SqlClient;
using Fusap.Common.Data;
using Fusap.Common.SqlServer;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class FusapSqlServerExtensions
    {
        public static IServiceCollection AddFusapSqlServer(this IServiceCollection services)
        {
            // Register the default connection factory to use SqlConnection.
            services.AddFusapDatabase<SqlConnection>();

            // Configure the default options for SqlServer.
            services.ConfigureOptions<FusapSqlServerDatabaseConfigureOptions>();

            // Configure Sql Server health checks.
            services.AddHealthChecks()
                .AddSqlServer(provider =>
                    provider.GetRequiredService<IOptions<FusapDatabaseOptions>>().Value.ConnectionString,
                    name: "sql-server-write")
                .AddSqlServer(provider =>
                    provider.GetRequiredService<IOptions<FusapDatabaseOptions>>().Value.ReadOnlyConnectionString,
                    name: "sql-server-read-only");

            return services;
        }
    }
}