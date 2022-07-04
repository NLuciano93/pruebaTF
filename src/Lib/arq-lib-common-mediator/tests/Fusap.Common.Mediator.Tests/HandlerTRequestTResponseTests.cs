using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Model;
using Xunit;

namespace Fusap.Common.Mediator.Tests
{
    public class HandlerTRequestTResponseTests
    {
        [Fact]
        public async Task Handle_WithFaultyHandler_ReturnsError()
        {
            // Arrange
            var request = new TestRequestGuid();
            var handler = (IHandler<TestRequestGuid, Guid>)new FaultyTestRequestGuidHandler();

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(response.IsSuccessful());
            Assert.True(response.Error is UnexpectedError);
        }

        [Fact]
        public async Task Handle_WithWorkingHandler_ReturnsSuccess()
        {
            // Arrange
            var request = new TestRequestGuid();
            var handler = (IHandler<TestRequestGuid, Guid>)new WorkingTestRequestGuidHandler();

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(response.IsSuccessful());
        }
    }
}
