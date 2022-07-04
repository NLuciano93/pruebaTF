using System.Data;
using System.Data.Common;

namespace Fusap.Common.Data
{
    public class ReadOnlyDbConnection : DelegatedDbConnection, IReadOnlyDbConnection
    {
        public ReadOnlyDbConnection(DbConnection connection) : base(connection)
        {
        }
    }
}