using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Formatting;

namespace Fusap.Common.Logger.Tests.Support
{
    internal static class Assertions
    {
        private static readonly JsonSerializerSettings SETTINGS = new JsonSerializerSettings
        {
            DateParseHandling = DateParseHandling.None
        };

        public static JObject AssertValidJson(ITextFormatter formatter, Action<ILogger> act)
        {
            var output = new StringWriter();
            var log = new LoggerConfiguration()
                .WriteTo.Sink(new TextWriterSink(output, formatter))
                .CreateLogger();

            act(log);

            var json = output.ToString();

            // Unfortunately this will not detect all JSON formatting issues; better than nothing however.
            return JsonConvert.DeserializeObject<JObject>(json, SETTINGS);
        }
    }
}