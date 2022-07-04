using System.IO;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Fusap.Common.Logger.Tests.Support
{
    public class TextWriterSink : ILogEventSink
    {
        private readonly StringWriter _output;
        private readonly ITextFormatter _formatter;

        public TextWriterSink(StringWriter output, ITextFormatter formatter)
        {
            _output = output;
            _formatter = formatter;
        }

        public void Emit(LogEvent logEvent)
        {
            _formatter.Format(logEvent, _output);
        }
    }
}