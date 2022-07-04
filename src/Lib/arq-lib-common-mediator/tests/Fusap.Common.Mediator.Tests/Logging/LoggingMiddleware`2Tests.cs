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
    public class LoggingMiddleware2Tests
    {
        readonly Mock<ILogger<TestRequestGuid>> _logger;
        readonly LoggingMiddleware<TestRequestGuid, Guid> _sut;

        public LoggingMiddleware2Tests()
        {
            _logger = new Mock<ILogger<TestRequestGuid>>(MockBehavior.Strict);

            var disposable = new Mock<IDisposable>();

            _logger
                .Setup(x => x.BeginScope(It.IsAny<FormattedLogValues>()))
                .Returns(disposable.Object);
            _logger
                .Setup(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(),
                    It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));

            _sut = new LoggingMiddleware<TestRequestGuid, Guid>(_logger.Object);
        }

        [Fact]
        public async Task Handle_WithSuccess_LogsSuccess()
        {
            // Arrange
            var next = (RequestHandlerDelegate<IResult<Guid>>)(() => Task.FromResult((IResult<Guid>)Result.Success(Guid.Empty)));
            var request = new TestRequestGuid();

            // Act
            await _sut.Handle(request, CancellationToken.None, next);

            // Assert
            _logger.VerifyAll();
        }

        [Fact]
        public async Task Handle_WithError_LogsError()
        {
            // Arrange
            var next = (RequestHandlerDelegate<IResult<Guid>>)(() => Task.FromResult((IResult<Guid>)new Result<Guid>(new NotFoundError("a", "b"))));
            var request = new TestRequestGuid();

            // Act
            await _sut.Handle(request, CancellationToken.None, next);

            // Assert
            _logger.VerifyAll();
        }


        [Fact]
        public async Task Handle_WithException_LogsError()
        {
            // Arrange
            var next = (RequestHandlerDelegate<IResult<Guid>>)(() => Task.FromException<IResult<Guid>>(new NotImplementedException()));
            var request = new TestRequestGuid();

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
