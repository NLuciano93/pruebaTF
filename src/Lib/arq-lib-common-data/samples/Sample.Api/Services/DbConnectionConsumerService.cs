using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Sample.Api.Services
{
    public class DbConnectionConsumerService : IService
    {
        private readonly IDbConnection _connection;
        private readonly IReadOnlyDbConnection _readOnlyConnection;

        public DbConnectionConsumerService(IDbConnection connection, IReadOnlyDbConnection readOnlyConnection)
        {
            _connection = connection;
            _readOnlyConnection = readOnlyConnection;
        }

        public async Task DoWorkAsync()
        {
            // Run queries on a read replica
            await _readOnlyConnection.QueryAsync("SELECT SERVERPROPERTY('productversion')");

            // Modify the write replica
            await _connection.QueryAsync("SELECT SERVERPROPERTY('productversion')");

            // Sync version
            // ReSharper disable once MethodHasAsyncOverload
            _connection.Execute("SELECT SERVERPROPERTY('productversion')");
        }
    }
}