using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace Fusap.Common.Logger.LogTypes
{
    internal class LogTypeEnricher : ILogEventEnricher
    {
        private const string PROPERTY_NAME = "LogType";

        private static readonly Dictionary<string, string> LOG_TYPE_MAPPINGS = new Dictionary<string, string>
        {
            { "Serilog.AspNetCore.RequestLoggingMiddleware", "Activity" },
            { "Fusap.Common.Logger.LogTypes.ActivityLog", "Activity" },
            { "Fusap.Common.Logger.LogTypes.FunctionalLog", "Functional" },
            { "Fusap.Common.Logger.LogTypes.SecurityLog", "Security" },
            { "Fusap.Common.Logger.LogTypes.TechnicalLog", "Technical" }
        };

        private const string SOURCE_CONTEXT_PROPERTY = "SourceContext";

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Properties.ContainsKey(PROPERTY_NAME))
            {
                return;
            }

            if (!logEvent.Properties.ContainsKey(SOURCE_CONTEXT_PROPERTY))
            {
                return;
            }

            if (!(logEvent.Properties[SOURCE_CONTEXT_PROPERTY] is ScalarValue sourceContext))
            {
                return;
            }

            var logTypeValue = GetLogType(sourceContext.Value.ToString());

            var logTypeProperty = propertyFactory.CreateProperty(PROPERTY_NAME, logTypeValue);
            logEvent.AddPropertyIfAbsent(logTypeProperty);
        }

        internal static string GetLogType(string value)
        {
            return LOG_TYPE_MAPPINGS.ContainsKey(value)
                ? LOG_TYPE_MAPPINGS[value]
                : "Technical";
        }
    }
}