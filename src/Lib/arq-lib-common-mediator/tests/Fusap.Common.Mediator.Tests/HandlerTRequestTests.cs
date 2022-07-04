using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Model;
using Xunit;

namespace Fusap.Common.Mediator.Tests
{
    public class HandlerTRequestTests
    {
        [Fact]
        public async Task Handle_WithFaultyHandler_ReturnsError()
        {
            // Arrange
            var request = new TestRequest();
            var handler = (IHandler<TestRequest>)new FaultyTestRequestHandler();

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
            var request = new TestRequest();
            var handler = (IHandler<TestRequest>)new WorkingTestRequestHandler();

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(response.IsSuccessful());
        }
    }
}
