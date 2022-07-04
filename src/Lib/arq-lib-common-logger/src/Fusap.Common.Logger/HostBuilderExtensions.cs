using System;
using System.Collections.Generic;
using System.Linq;
using Fusap.Common.Logger;
using Fusap.Common.Logger.LogTypes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Logging.EventSource;
using Microsoft.Extensions.Primitives;
using Serilog;
using Serilog.Events;

// This namespace is Microsoft.Extensions.Hosting in order to simplify the usage
// by reducing the number of using statements that frequently pollute the beginning
// of the startup files.
// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseFusapLogger(this IHostBuilder hostBuilder)
        {
            // Configure default options
            hostBuilder
                .ConfigureHostConfiguration(configurationBuilder =>
                {
                    var memorySource = new MemoryConfigurationSource
                    {
                        InitialData = new Dictionary<string, string>
                        {
                            // Debug
                            { "Serilog:WriteTo:0:Name", "Debug" },

                            // Console human-readable
                            { "Serilog:WriteTo:1:Name", "Console" },
                            //{ "Serilog:WriteTo:1:Args:OutputTemplate", "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}" },

                            // Console machine-readable
                            //{ "Serilog:WriteTo:1:Name", "Console" },
                            //{ "Serilog:WriteTo:1:Args:Formatter", "Fusap.Common.Logger.FusapJsonLogFormatter, Fusap.Common.Logger" },

                            // Kafka
                            //{ "Serilog:WriteTo:2:Name", "Kafka" },
                            //{ "Serilog:WriteTo:2:Args:Formatter", "Fusap.Common.Logger.FusapJsonLogFormatter, Fusap.Common.Logger" },
                        }
                    };

                    configurationBuilder.Sources.Insert(0, memorySource);
                });

            // Removes loggers that Serilog writes directly into.
            Type[] typesToRemove = { typeof(DebugLoggerProvider), typeof(ConsoleLoggerProvider), typeof(EventSourceLoggerProvider) };
            hostBuilder.ConfigureLogging(builder => RemoveServices(builder.Services, typesToRemove));

            // Configure initial Serilog logger and allow logs to be propagated to all other registered ILoggers.
            hostBuilder.UseSerilog(ConfigureLogger, writeToProviders: true);

            hostBuilder.ConfigureServices((context, services) =>
            {
                // Register the IStartupFilter that is responsible for logging request details.
                services.AddTransient<IStartupFilter, FusapRequestLoggingStartupFilter>();

                // Recreate logger when the configuration gets reloaded.
                ChangeToken.OnChange(
                    () => context.Configuration.GetReloadToken(),
                    () =>
                    {
                        var loggerConfig = new LoggerConfiguration();
                        ConfigureLogger(context, loggerConfig);
                        Log.Logger = loggerConfig.CreateLogger();
                        Log.Information("Serilog reloaded");
                    }
                );
            });

            return hostBuilder;
        }

        private static void RemoveServices(IServiceCollection services, params Type[] typesToRemove)
        {
            foreach (var type in typesToRemove)
            {
                var service = services.FirstOrDefault(d => d.ImplementationType == type);
                if (service != null)
                {
                    services.Remove(service);
                }
            }
        }

        private static void ConfigureLogger(HostBuilderContext context, LoggerConfiguration config)
        {
            KafkaSinkPreConfigurator.PreConfigure(context.Configuration);
            Serilog.Debugging.SelfLog.Enable(Console.Error);

            config.ReadFrom.Configuration(context.Configuration)
                // Override the minimum level loaded with the minimum level requested by one of the sinks defined on the configuration
                .MinimumLevel.Is(GetMinimumEffectiveLevel(context.Configuration))
                .Enrich.FromLogContext()
                .Enrich.WithAppName(context.Configuration)
                .Enrich.WithPlatform(context.Configuration)
                .Enrich.WithEnvironment(context.HostingEnvironment)
                .Enrich.WithMachineName()
                .Enrich.WithAppVersion()
                .Enrich.With<EventIdEnricher>()
                .Enrich.With<LogTypeEnricher>()
                ;
        }

        // The purpose of this is to provide an upgrade path for projects that were using the version that configured the sinks in code
        // instead of relying on IConfiguration. Previously all sinks had to use the same MinimumLevel and now, since each sink is
        // configured independently, we can set different logging levels as well.
        // This function can be removed once most project upgrade.
        private static LogEventLevel GetMinimumEffectiveLevel(IConfiguration configuration)
        {
            var value = configuration.GetValue("Serilog:MinimumLevel:Default", LogEventLevel.Information);

            var sinks = SinkConfigurationReader.GetAllSinkConfigurationSections(configuration);

            foreach (var sink in sinks)
            {
                // Use the highest level in case it is not specified as to not artificially lower the global setting.
                var sinkMinimum = sink.GetValue("args:restrictedToMinimumLevel", LogEventLevel.Fatal);

                if ((int)value > (int)sinkMinimum)
                {
                    value = sinkMinimum;
                }
            }

            return value;
        }
    }
}
