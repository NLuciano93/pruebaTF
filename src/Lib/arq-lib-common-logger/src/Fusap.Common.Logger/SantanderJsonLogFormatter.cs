using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;

namespace Fusap.Common.Logger
{
    public class SantanderJsonLogFormatter : ITextFormatter
    {
        private readonly JsonValueFormatter _valueFormatter;

        private static readonly HashSet<string> BASE_PROPERTIES = new HashSet<string>
        {
            "RequestMethod",
            "RequestPath",
            "StatusCode",
            "SpanId",
            "TraceId",
            "ParentId",
            "AppName",
            "AppVersion",
            "Platform",
            "Env",
            "LogType"
        };

        private static readonly Dictionary<string, string> BASE_PROPERTY_REMAPPING = new Dictionary<string, string>
        {
            { "AppName", "appName" },
            { "Platform", "platform" },
            { "TraceId", "traceId" },
            { "SpanId", "spanId" },
            { "ParentId", "parentSpanId" },
            { "LogType", "logType" },
            { "Env", "environment" },
            { "RequestMethod", "method" },
            { "RequestPath", "url" },
            { "StatusCode", "returnCode" },
        };

        public SantanderJsonLogFormatter(JsonValueFormatter valueFormatter = null)
        {
            _valueFormatter = valueFormatter ?? new JsonValueFormatter(typeTagName: "$type");
        }

        public void Format(LogEvent logEvent, TextWriter output)
        {
            FormatEvent(logEvent, output, _valueFormatter);
            output.WriteLine();
        }

        public static void FormatEvent(LogEvent logEvent, TextWriter output, JsonValueFormatter valueFormatter)
        {
            if (logEvent == null)
            {
                throw new ArgumentNullException(nameof(logEvent));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (valueFormatter == null)
            {
                throw new ArgumentNullException(nameof(valueFormatter));
            }

            output.Write('{');

            WriteTimestamp(logEvent, output);
            WriteMessage(logEvent, output);
            WriteLogLevel(logEvent, output);
            WriteException(logEvent, output);
            WriteBaseLogProperties(logEvent, output, valueFormatter);
            WriteLogProperties(logEvent, output, valueFormatter);

            output.Write('}');
        }

        private static void WriteTimestamp(LogEvent logEvent, TextWriter output)
        {
            output.Write("\"timestamp\":\"");
            output.Write(logEvent.Timestamp.UtcDateTime.ToString("O"));
        }

        private static void WriteMessage(LogEvent logEvent, TextWriter output)
        {
            output.Write("\",\"log\":");
            var message = logEvent.MessageTemplate.Render(logEvent.Properties);
            JsonValueFormatter.WriteQuotedJsonString(message, output);
        }

        private static void WriteLogLevel(LogEvent logEvent, TextWriter output)
        {
            output.Write(",\"loglevel\":\"");
            output.Write(logEvent.Level);
            output.Write('\"');
        }

        private static void WriteException(LogEvent logEvent, TextWriter output)
        {
            if (logEvent.Exception == null)
            {
                return;
            }

            output.Write(",\"error\":");
            JsonValueFormatter.WriteQuotedJsonString(logEvent.Exception.ToString(), output);
        }

        private static void WriteBaseLogProperties(LogEvent logEvent, TextWriter output, JsonValueFormatter valueFormatter)
        {
            var baseEventProperties = logEvent.Properties.Where(p => BASE_PROPERTIES.Contains(p.Key));
            foreach (var property in baseEventProperties)
            {
                WriteProperty(output, valueFormatter, property);
            }
        }

        private static void WriteLogProperties(LogEvent logEvent, TextWriter output, JsonValueFormatter valueFormatter)
        {
            var customLogProperties = logEvent.Properties.Where(p => !BASE_PROPERTIES.Contains(p.Key)).ToArray();
            if (customLogProperties.Length <= 0)
            {
                return;
            }

            output.Write(",\"customLog\":{");
            var i = 0;

            foreach (var property in customLogProperties)
            {
                WriteProperty(output, valueFormatter, property, i++ > 0);
            }

            output.Write('}');
        }

        private static void WriteProperty(TextWriter output, JsonValueFormatter valueFormatter, KeyValuePair<string, LogEventPropertyValue> property, bool writeSeparator = true)
        {
            var name = property.Key;

            if (BASE_PROPERTY_REMAPPING.ContainsKey(name))
            {
                name = BASE_PROPERTY_REMAPPING[name];
            }

            if (name.Length > 0 && name[0] == '@')
            {
                // Escape first '@' by doubling
                name = '@' + name;
            }

            if (writeSeparator)
            {
                output.Write(',');
            }

            JsonValueFormatter.WriteQuotedJsonString(name, output);
            output.Write(':');
            valueFormatter.Format(property.Value, output);
        }
    }
}