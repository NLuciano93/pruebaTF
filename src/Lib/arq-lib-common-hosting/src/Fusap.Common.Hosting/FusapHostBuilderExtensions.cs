using System;
using System.Collections.Generic;
using System.Diagnostics;
using Fusap.Common.Hosting;
using Fusap.Common.Hosting.HealthChecks;
using Fusap.Common.Hosting.Refresh;
using Fusap.Common.Hosting.UserAgent;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;

// This namespace is Microsoft.Extensions.Hosting in order to simplify the usage
// by reducing the number of using statements that frequently pollute the beginning
// of the startup files.
// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting
{
    public static class FusapHostBuilderExtensions
    {
        private const string FUSAP_HOST_VALIDATED_PROPERTY = "FusapHostValidated";


        public static IHostBuilder ConfigureFusapHost(this IHostBuilder hostBuilder, Action<FusapHostOptions> configureOptions)
        {
            var options = new FusapHostOptions();
            configureOptions(options);
            FusapHostOptions.Validate(options);

            // Set default IdFormat.
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            hostBuilder
                // Configure global default options that are common across all projects
                .ConfigureHostConfiguration(configurationBuilder =>
                {
                    var memorySource = new MemoryConfigurationSource
                    {
                        InitialData = new Dictionary<string, string>
                        {
                            // Make application name available for general purposes
                            { "fusap:application:name", options.ApplicationName },

                            // Setup defaults for Spring Cloud Config
                            { "spring:application:name", options.ApplicationName },
                            // The cloud config uri should be passed by environment variable by k8s
                            // { "spring:cloud:config:uri", "http://arq-config-server.config:8888" },
                            { "spring:cloud:config:failFast", "true" },

                            // Setup defaults for Jaeger
                            { "JAEGER_SERVICE_NAME", options.ApplicationName },
                            { "JAEGER_SAMPLER_TYPE", "const" },

                            // Setup Steeltoe refresh actuator
                            { "management:endpoints:path", "/" },
                            { "management:endpoints:actuator:exposure:include:0", "refresh" }
                        }
                    };

                    configurationBuilder.Sources.Insert(0, memorySource);
                })

                // Add configuration refresh endpoint
                .AddFusapRefreshActuator()

                // Add standardized logging
                .UseFusapLogger()

                // Register basic services
                .ConfigureServices((context, services) =>
                {
                    // Making FusapHostOptions available through DI
                    services.Configure<FusapHostOptions>(fusapHostOptions =>
                    {
                        configureOptions(fusapHostOptions);
                        FusapHostOptions.Validate(fusapHostOptions);
                    });

                    // Add user-agent http request injector
                    services.TryAddEnumerable(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, UserAgentInjectorBuilderFilter>());

                    // Add basic healthcheck support and setup default reporting
                    services.AddFusapHealthChecks();

                    // Remove server:kestrel response header
                    services.Configure<KestrelServerOptions>(kestrelServerOptions =>
                    {
                        kestrelServerOptions.AddServerHeader = false;
                    });
                })

                ;

            return hostBuilder;
        }

        public static IHostBuilder ValidateFusapHost(this IHostBuilder hostBuilder)
        {
            if (hostBuilder.Properties.ContainsKey(FUSAP_HOST_VALIDATED_PROPERTY))
            {
                throw new NotSupportedException("You must not use ConfigureFusapWebApi and ConfigureFusapWorker at the same time.");
            }


            hostBuilder.Properties[FUSAP_HOST_VALIDATED_PROPERTY] = true;

            return hostBuilder;
        }
    }
}
