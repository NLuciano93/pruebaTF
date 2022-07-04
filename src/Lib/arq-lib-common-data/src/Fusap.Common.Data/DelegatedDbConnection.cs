using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Fusap.Common.Data
{
    public abstract class DelegatedDbConnection : DbConnection
    {
        private readonly DbConnection _connection;

        protected DelegatedDbConnection(DbConnection connection)
        {
            _connection = connection;
            connection.StateChange += Connection_StateChange;
        }

        private void Connection_StateChange(object sender, StateChangeEventArgs e)
        {
            OnStateChange(e);
        }

        public override string ConnectionString
        {
            get => _connection.ConnectionString;
            set => _connection.ConnectionString = value;
        }
        public override string Database => _connection.Database;
        public override string DataSource => _connection.DataSource;
        public override string ServerVersion => _connection.ServerVersion;
        public override ConnectionState State => _connection.State;

        public override void ChangeDatabase(string databaseName)
        {
            _connection.ChangeDatabase(databaseName);
        }

        public override void Close()
        {
            _connection.Close();
        }

        public override void Open()
        {
            _connection.Open();
        }

        protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
        {
            return _connection.BeginTransaction(isolationLevel);
        }

        protected override DbCommand CreateDbCommand()
        {
            return _connection.CreateCommand();
        }

        public override int ConnectionTimeout => _connection.ConnectionTimeout;

        public override void EnlistTransaction(Transaction transaction)
        {
            _connection.EnlistTransaction(transaction);
        }

        public override bool Equals(object obj)
        {
            return _connection.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _connection.GetHashCode();
        }

        public override DataTable GetSchema()
        {
            return _connection.GetSchema();
        }

        public override DataTable GetSchema(string collectionName)
        {
            return _connection.GetSchema(collectionName);
        }

        public override DataTable GetSchema(string collectionName, string[] restrictionValues)
        {
            return _connection.GetSchema(collectionName, restrictionValues);
        }

        public override object InitializeLifetimeService()
        {
            return _connection.InitializeLifetimeService();
        }

        public override Task OpenAsync(CancellationToken cancellationToken)
        {
            return _connection.OpenAsync(cancellationToken);
        }

        public override ISite Site
        {
            get => _connection.Site;
            set => _connection.Site = value;
        }

        public override string ToString()
        {
            return _connection.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            _connection.Dispose();
        }
    }
}