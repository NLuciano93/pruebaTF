using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Configuration;

namespace Fusap.Common.Logger
{
    public static class LoggerEnrichmentConfigurationExtensions
    {
        public static LoggerConfiguration WithAppName(this LoggerEnrichmentConfiguration loggerConfiguration, IConfiguration configuration)
        {
            return loggerConfiguration.WithProperty("AppName", configuration["fusap:application:name"]
                                                               ?? configuration["spring:application:name"]);
        }

        public static LoggerConfiguration WithAppVersion(this LoggerEnrichmentConfiguration loggerConfiguration)
        {
            var infoVersion = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var version = infoVersion?.InformationalVersion ?? Assembly.GetEntryAssembly()?.GetName().Version.ToString() ?? "Unknown";

            return loggerConfiguration.WithProperty("AppVersion", version);
        }

        public static LoggerConfiguration WithPlatform(this LoggerEnrichmentConfiguration loggerConfiguration, IConfiguration configuration)
        {
            return loggerConfiguration.WithProperty("Platform", configuration["fusap:application:platform"]
                                                                ?? configuration["spring:application:platform"]);
        }

        public static LoggerConfiguration WithEnvironment(this LoggerEnrichmentConfiguration loggerConfiguration, IHostEnvironment hostEnvironment)
        {
            return loggerConfiguration.WithProperty("Env", hostEnvironment.EnvironmentName);
        }
    }
}