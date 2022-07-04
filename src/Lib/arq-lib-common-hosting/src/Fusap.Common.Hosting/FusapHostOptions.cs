using System;
using System.Reflection;

namespace Fusap.Common.Hosting
{
    public class FusapHostOptions
    {
        public string ApplicationName { get; set; } = Assembly.GetEntryAssembly()?.FullName ?? "fusap-unknown-app";

        internal static void Validate(FusapHostOptions options)
        {
            if (string.IsNullOrEmpty(options.ApplicationName))
            {
                throw new ArgumentNullException(nameof(ApplicationName));
            }
        }
    }
}