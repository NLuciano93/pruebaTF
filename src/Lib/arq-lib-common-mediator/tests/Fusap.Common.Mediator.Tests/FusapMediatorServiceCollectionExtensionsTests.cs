using System;
using Microsoft.Extensions.DependencyInjection;
using Fusap.Common.Mediator.Logging;
using Fusap.Common.Mediator.Tracing;
using Xunit;

namespace Fusap.Common.Mediator.Tests
{
    public class FusapMediatorServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddFusapMediator_WithLoggingAndTracing_RegisterMiddleware()
        {
            // Arrange

            var services = new ServiceCollection();
            var assemblies = new[] { typeof(FusapMediatorServiceCollectionExtensionsTests).Assembly };

            // Act
            services.AddFusapMediator(assemblies, opt =>
            {
                opt.UseLogging();
                opt.UseTracing();
            });

            // Assert
            Assert.Contains(services, d => d.ImplementationType == typeof(LoggingMiddleware<TestRequest>));
            Assert.Contains(services, d => d.ImplementationType == typeof(LoggingMiddleware<TestRequestGuid, Guid>));
            Assert.Contains(services, d => d.ImplementationType == typeof(TracingMiddleware<TestRequest>));
            Assert.Contains(services, d => d.ImplementationType == typeof(TracingMiddleware<TestRequestGuid, Guid>));
        }
    }
}
