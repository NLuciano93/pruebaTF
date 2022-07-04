using System;
using System.Data.Common;

namespace Fusap.Common.Data
{
    public static class DbConnectionStringBuilderExtensions
    {
        public static string? GetComponent(this DbConnectionStringBuilder builder, string key)
        {
            return builder.ContainsKey(key) ? builder[key].ToString() : null;
        }

        public static string GetRequiredComponent(this DbConnectionStringBuilder builder, string key)
        {
            return builder.GetComponent(key) ??
                   throw new ArgumentException($"Connection string is missing required component: {key}");
        }
    }
}