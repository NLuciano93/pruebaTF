using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Fusap.Common.Logger
{
    internal static class SinkConfigurationReader
    {
        internal static IEnumerable<IConfigurationSection> GetAllSinkConfigurationSections(IConfiguration configuration)
        {
            return configuration.GetSection("Serilog:WriteTo").GetChildren();
        }

        internal static IConfigurationSection GetSinkConfigurationSectionOrDefault(IConfiguration configuration, string name)
        {
            return GetAllSinkConfigurationSections(configuration).FirstOrDefault(sink => IsSinkNameEquals(name, sink));
        }

        private static bool IsSinkNameEquals(string sinkName, IConfigurationSection sink)
        {
            return string.Equals(sinkName, sink.GetValue<string>("name"), StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
