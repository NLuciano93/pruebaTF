using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using OpenTracing;
using OpenTracing.Mock;
using Fusap.Common.Mediator.Tracing;
using Fusap.Common.Model;
using Xunit;

namespace Fusap.Common.Mediator.Tests.Tracing
{
    public class TracingMiddleware1Tests
    {
        private readonly Mock<ITracer> _tracer;
        private readonly TracingMiddleware<TestRequest> _sut;

        public TracingMiddleware1Tests()
        {
            var mockTracer = new MockTracer();

            _tracer = new Mock<ITracer>(MockBehavior.Strict);

            _tracer
                .Setup(x => x.BuildSpan(It.IsAny<string>()))
                .Returns(mockTracer.BuildSpan("any"));

            _sut = new TracingMiddleware<TestRequest>(_tracer.Object);
        }

        [Fact]
        public async Task Handle_WithSuccess_LogsSuccess()
        {
            // Arrange
            var next = (RequestHandlerDelegate<IResult>)(() => Task.FromResult((IResult)Result.Success()));
            var request = new TestRequest();

            // Act
            await _sut.Handle(request, CancellationToken.None, next);

            // Assert
            _tracer.VerifyAll();
        }

        [Fact]
        public async Task Handle_WithError_LogsError()
        {
            // Arrange
            var next = (RequestHandlerDelegate<IResult>)(() => Task.FromResult((IResult)Result.Failure(new UnexpectedError("a", "b"))));
            var request = new TestRequest();

            // Act
            await _sut.Handle(request, CancellationToken.None, next);

            // Assert
            _tracer.VerifyAll();
        }

        [Fact]
        public async Task Handle_WithException_LogsError()
        {
            // Arrange
            var next = (RequestHandlerDelegate<IResult>)(() => Task.FromException<IResult>(new NotImplementedException()));
            var request = new TestRequest();

            // Act
            Task Act()
            {
                return _sut.Handle(request, CancellationToken.None, next);
            }

            // Assert
            await Assert.ThrowsAsync<NotImplementedException>(Act);
            _tracer.VerifyAll();
        }
    }
}
