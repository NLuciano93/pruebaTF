using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using Fusap.Common.Mediator.Logging;
using Fusap.Common.Model;
using Xunit;

namespace Fusap.Common.Mediator.Tests.Logging
{
    public class LoggingMiddleware1Tests
    {
        private readonly Mock<ILogger<TestRequest>> _logger;
        private readonly LoggingMiddleware<TestRequest> _sut;

        public LoggingMiddleware1Tests()
        {
            _logger = new Mock<ILogger<TestRequest>>(MockBehavior.Strict);

            var disposable = new Mock<IDisposable>();

            _logger
                .Setup(x => x.BeginScope(It.IsAny<FormattedLogValues>()))
                .Returns(disposable.Object);
            _logger
                .Setup(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(),
                    It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));

            _sut = new LoggingMiddleware<TestRequest>(_logger.Object);
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
            _logger.VerifyAll();
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
            _logger.VerifyAll();
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
            _logger.VerifyAll();
        }
    }
}
