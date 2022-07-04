using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Serilog;
using Serilog.Events;

namespace Fusap.Common.Logger
{
    public class FusapRequestLoggingStartupFilter : IStartupFilter
    {
        private const string DefaultDisplayName = "Health checks";

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseSerilogRequestLogging(opt => opt.GetLevel = GetLogEventLevel);

                next(app);
            };
        }

        private static LogEventLevel GetLogEventLevel(HttpContext context, double _, Exception exception)
        {
            if (exception != null || context.Response.StatusCode > 499)
            {
                return LogEventLevel.Error;
            }

            if (IsHealthCheckEndpoint(context))
            {
                return LogEventLevel.Verbose;
            }

            return LogEventLevel.Information;
        }

        private static bool IsHealthCheckEndpoint(HttpContext context)
        {
            return string.Equals(context?.Features.Get<IEndpointFeature>()?.Endpoint?.DisplayName, DefaultDisplayName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}