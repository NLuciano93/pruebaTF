using System.Data;

namespace Fusap.TimeSheet.Data
{
    public partial class DbConnectionConsumerService : IService
    {
        private readonly IDbConnection _connection;
        private readonly IReadOnlyDbConnection _readOnlyConnection;

        public DbConnectionConsumerService(IDbConnection connection, IReadOnlyDbConnection readOnlyConnection)
        {
            _connection = connection;
            _readOnlyConnection = readOnlyConnection;
        }
    }
}
