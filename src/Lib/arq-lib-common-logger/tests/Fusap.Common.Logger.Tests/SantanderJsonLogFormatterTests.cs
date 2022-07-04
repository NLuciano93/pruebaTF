using System;
using Newtonsoft.Json.Linq;
using Serilog;
using Fusap.Common.Logger.Tests.Support;
using Xunit;

namespace Fusap.Common.Logger.Tests
{
    public class SantanderJsonLogFormatterTests
    {
        private static JObject AssertValidJson(Action<ILogger> act)
        {
            return Assertions.AssertValidJson(new SantanderJsonLogFormatter(), act);
        }

        [Fact]
        public void AnEmptyEventIsValidJson()
        {
            AssertValidJson(log => log.Information("No properties"));
        }

        [Fact]
        public void AMinimalEventIsValidJson()
        {
            var jobject = AssertValidJson(log => log.Information("One {Property}", 42));

            Assert.Equal("One 42", jobject["log"]);
        }

        [Fact]
        public void MultiplePropertiesAreDelimited()
        {
            AssertValidJson(log => log.Information("Property {First} and {Second}", "One", "Two"));
        }

        [Fact]
        public void ExceptionsAreFormattedToValidJson()
        {
            AssertValidJson(log => log.Information(new DivideByZeroException(), "With exception"));
        }

        [Fact]
        public void ExceptionAndPropertiesAreValidJson()
        {
            AssertValidJson(log => log.Information(new DivideByZeroException(), "With exception and {Property}", 42));
        }

        [Fact]
        public void RenderingsAreValidJson()
        {
            AssertValidJson(log => log.Information("One {Rendering:x8}", 42));
        }

        [Fact]
        public void MultipleRenderingsAreDelimited()
        {
            AssertValidJson(log => log.Information("Rendering {First:x8} and {Second:x8}", 1, 2));
        }

        [Fact]
        public void LogsOtherThanInfoGenerateLogLevelProperty()
        {
            var jobject = AssertValidJson(log => log.Warning("No properties"));

            Assert.True(jobject.TryGetValue("loglevel", out var level));
            Assert.Equal("Warning", level.ToObject<string>());
        }

        [Fact]
        public void CustomPropertiesAreSentToCustomLogObject()
        {
            // Not possible in message templates, but accepted this way
            var jobject = AssertValidJson(log => log.ForContext("RandomProperty", "RandomPropertyValue")
                                                    .Information("Hello"));

            var customLogObject = jobject.GetValue("customLog");

            var val = customLogObject["RandomProperty"];
            Assert.Equal("RandomPropertyValue", val.ToObject<string>());
        }

        [Fact]
        public void AtPrefixedPropertyNamesAreEscaped()
        {
            // Not possible in message templates, but accepted this way
            var jobject = AssertValidJson(log => log.ForContext("@Mistake", 42)
                                                    .Information("Hello"));

            var customLogObject = jobject.GetValue("customLog");

            var val = customLogObject["@@Mistake"];
            Assert.Equal(42, val.ToObject<int>());
        }

        [Fact]
        public void RequestMethodIsLoggedAsValidProperty()
        {
            TestProperty("RequestMethod", "GET", "method");
        }

        [Fact]
        public void RequestPathIsLoggedAsValidProperty()
        {
            TestProperty("RequestPath", "/path", "url");
        }

        [Fact]
        public void StatusCodeIsLoggedAsValidProperty()
        {
            TestProperty("StatusCode", 200, "returnCode");
        }

        [Fact]
        public void ElapsedIsLoggedAsValidProperty()
        {
            TestCustomLogProperty("Elapsed", "200ms");
        }

        [Fact]
        public void RequestIdIsLoggedAsValidProperty()
        {
            TestCustomLogProperty("RequestId", "1234");
        }

        [Fact]
        public void SpanIdIsLoggedAsValidProperty()
        {
            TestProperty("SpanId", "1234", "spanId");
        }

        [Fact]
        public void TraceIdIsLoggedAsValidProperty()
        {
            TestProperty("TraceId", "1234", "traceId");
        }

        [Fact]
        public void ParentIdIsLoggedAsValidProperty()
        {
            TestProperty("ParentId", "1234", "parentSpanId");
        }

        [Fact]
        public void ConnectionIdIsLoggedAsValidProperty()
        {
            TestCustomLogProperty("ConnectionId", "1234");
        }

        [Fact]
        public void AppNameIsLoggedAsValidProperty()
        {
            TestProperty("AppName", "MyApp", "appName");
        }

        private void TestProperty<T>(string name, T value, string mappedName = null)
        {
            var jobject = AssertValidJson(log => log.ForContext(name, value)
                .Information("Hello"));

            Assert.True(jobject.TryGetValue(mappedName ?? name, out var val));
            Assert.Equal(value, val.ToObject<T>());
        }

        private void TestCustomLogProperty<T>(string name, T value)
        {
            var jobject = AssertValidJson(log => log.ForContext(name, value)
                .Information("Hello"));

            Assert.Equal(value, jobject["customLog"][name].ToObject<T>());
        }
    }
}