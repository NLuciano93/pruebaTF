using System;
using System.Data;
using System.Data.Common;
using Microsoft.Extensions.Options;

namespace Fusap.Common.Data
{
    public class FusapDatabase : IFusapDatabase
    {
        private readonly Func<string, DbConnection> _createDbConnection;
        private readonly IOptions<FusapDatabaseOptions> _options;
        private readonly IServiceProvider _serviceProvider;

        public FusapDatabase(Func<string, DbConnection> createDbConnection, IOptions<FusapDatabaseOptions> options, IServiceProvider serviceProvider)
        {
            _createDbConnection = createDbConnection;
            _options = options;
            _serviceProvider = serviceProvider;
        }

        public IDbConnection CreateConnection()
        {
            return CreateConnection(_options.Value.ConnectionString);
        }

        public IDbConnection CreateReadOnlyConnection()
        {
            return CreateConnection(_options.Value.ReadOnlyConnectionString);
        }

        protected virtual IDbConnection CreateConnection(string connectionString)
        {
            var connection = _createDbConnection(connectionString);
            return connection;
        }
    }

    public class FusapDatabase<TDbConnection> : FusapDatabase where TDbConnection : DbConnection, new()
    {
        public FusapDatabase(
            IOptions<FusapDatabaseOptions> options,
            IServiceProvider serviceProvider
        ) : base(ConnectionFactory, options, serviceProvider)
        {
        }

        private static DbConnection ConnectionFactory(string connectionString)
        {
            return new TDbConnection
            {
                ConnectionString = connectionString
            };
        }
    }
}
