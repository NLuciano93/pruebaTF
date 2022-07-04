using System.Data;

namespace Fusap.Common.Data
{
    public interface IFusapDatabase
    {
        IDbConnection CreateConnection();
        IDbConnection CreateReadOnlyConnection();
    }
}