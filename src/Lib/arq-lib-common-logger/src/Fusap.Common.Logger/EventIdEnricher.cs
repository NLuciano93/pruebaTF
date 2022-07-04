using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Fusap.Common.Logger
{
    public class EventIdEnricher : ILogEventEnricher
    {
        private const string PROPERTY_NAME = "EventId";

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var id = EventIdHash.Compute(logEvent.MessageTemplate.Text);
            var property = propertyFactory.CreateProperty(PROPERTY_NAME, id.ToString("x8"));
            logEvent.AddOrUpdateProperty(property);
        }
    }
}