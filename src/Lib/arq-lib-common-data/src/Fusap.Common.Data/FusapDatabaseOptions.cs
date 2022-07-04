namespace Fusap.Common.Data
{
    public class FusapDatabaseOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string ReadOnlyConnectionString { get; set; } = string.Empty;
        public bool ActiveTracer { get; set; } = true;
    }
}