using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Fusap.Common.Data;

namespace Sample.Api.Services
{
    public class FusapDatabaseConsumerService : IService
    {
        private readonly IFusapDatabase _database;

        public FusapDatabaseConsumerService(IFusapDatabase database)
        {
            _database = database;
        }

        public async Task DoWorkAsync()
        {
            // Create task list
            var tasks = Enumerable.Range(0, 20)
                .Select(_ => ParallelWorkAsync())
                .ToArray();

            await Task.WhenAll(tasks);

            for (var i = 0; i < 20; i++)
            {
                // Sync version
                using (var connection = _database.CreateConnection())
                {
                    connection.Query("SELECT SERVERPROPERTY('productversion')");
                }

            }
        }

        public async Task ParallelWorkAsync()
        {
            // Be sure to dispose the created connections so they return to the pool.

            // Run queries on a read replica
            using (var readOnlyConnection = _database.CreateReadOnlyConnection())
            {
                await readOnlyConnection.QueryAsync("SELECT SERVERPROPERTY('productversion')");
            }

            // Modify the write replica
            using (var connection = _database.CreateConnection())
            {
                await connection.QueryAsync("SELECT SERVERPROPERTY('productversion')");
            }
        }
    }
}