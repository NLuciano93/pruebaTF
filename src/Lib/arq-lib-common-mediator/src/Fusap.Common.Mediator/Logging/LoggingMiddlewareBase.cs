using System;
using System.Diagnostics;
using Fusap.Common.Model;
using Microsoft.Extensions.Logging;

namespace Fusap.Common.Mediator.Logging
{
    public class LoggingMiddlewareBase
    {
        private readonly ILogger _logger;
        private IDisposable? _scope;
        private Stopwatch? _stopwatch;

        public LoggingMiddlewareBase(ILogger logger)
        {
            _logger = logger;
        }

        protected void LogStart(object request)
        {
            _scope = _logger.BeginScope("Handling request {RequestName}",
                request.GetType().Name);

            _logger.LogInformation("Handling request {RequestName} started",
                request.GetType().Name);
            _stopwatch = Stopwatch.StartNew();
        }

        protected void LogException(object request, Exception exception)
        {
            if (_scope == null || _stopwatch == null)
            {
                return;
            }

            _logger.LogError(exception, "Handling request {RequestName} failed in {ElapsedMilliseconds}ms",
                request.GetType().Name, _stopwatch.ElapsedMilliseconds);
            _scope.Dispose();
            _stopwatch.Stop();
        }

        protected void LogFinished(object request, IResult result)
        {
            if (_scope == null || _stopwatch == null)
            {
                return;
            }

            if (result.IsSuccessful())
            {
                _logger.LogInformation(
                    "Handling request {RequestName} finished in {ElapsedMilliseconds}ms successfully",
                    request.GetType().Name, _stopwatch.ElapsedMilliseconds);
            }
            else
            {
                _logger.LogWarning(
                    "Handling request {RequestName} finished in {ElapsedMilliseconds}ms with " +
                    "error {RequestErrorDescription} ({RequestErrorType})",
                    request.GetType().Name, _stopwatch.ElapsedMilliseconds,
                    result.Error!.ToString(), result.Error!.GetType().Name);
            }

            _scope.Dispose();
            _stopwatch.Stop();
        }
    }
}
