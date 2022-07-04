using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Fusap.Common.Data
{
    public abstract class DelegatedDbCommand : DbCommand
    {
        private readonly DbCommand _command;

        protected DelegatedDbCommand(DbCommand command)
        {
            _command = command;
        }

        public override void Cancel()
        {
            _command.Cancel();
        }

        public override string CommandText
        {
            get => _command.CommandText;
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
            set => _command.CommandText = value;
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
        }

        public override int CommandTimeout
        {
            get => _command.CommandTimeout;
            set => _command.CommandTimeout = value;
        }

        public override CommandType CommandType
        {
            get => _command.CommandType;
            set => _command.CommandType = value;
        }

        protected override DbParameter CreateDbParameter()
        {
            return _command.CreateParameter();
        }

        protected override DbConnection DbConnection
        {
            get => _command.Connection;
            set => _command.Connection = value;
        }

        protected override DbParameterCollection DbParameterCollection => _command.Parameters;

        protected override DbTransaction DbTransaction
        {
            get => _command.Transaction;
            set => _command.Transaction = value;
        }

        public override bool DesignTimeVisible
        {
            get => _command.DesignTimeVisible;
            set => _command.DesignTimeVisible = value;
        }

        protected override Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
        {
            return _command.ExecuteReaderAsync(behavior, cancellationToken);
        }

        public override Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
        {
            return _command.ExecuteNonQueryAsync(cancellationToken);
        }

        public override Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
        {
            return _command.ExecuteScalarAsync(cancellationToken);
        }

        public override Task PrepareAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return _command.PrepareAsync(cancellationToken);
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            return _command.ExecuteReader(behavior);
        }

        public override int ExecuteNonQuery()
        {
            return _command.ExecuteNonQuery();
        }

        public override object ExecuteScalar()
        {
            return _command.ExecuteScalar();
        }

        public override void Prepare()
        {
            _command.Prepare();
        }

        public override string ToString()
        {
            return _command.ToString();
        }
        public override ValueTask DisposeAsync()
        {
            return _command.DisposeAsync();
        }

        protected override void Dispose(bool disposing)
        {
            _command.Dispose();
        }

        public override UpdateRowSource UpdatedRowSource
        {
            get => _command.UpdatedRowSource;
            set => _command.UpdatedRowSource = value;
        }
    }
}