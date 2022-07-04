using Fusap.Common.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Fusap.Common.SqlServer
{
    public class FusapSqlServerDatabaseConfigureOptions : IConfigureOptions<FusapDatabaseOptions>
    {
        private readonly IConfiguration _configuration;

        public FusapSqlServerDatabaseConfigureOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(FusapDatabaseOptions options)
        {
            _configuration.GetSection("CQRSSettings:SqlServer").Bind(options);
            _configuration.GetSection("SqlServer").Bind(options);
        }
    }
}