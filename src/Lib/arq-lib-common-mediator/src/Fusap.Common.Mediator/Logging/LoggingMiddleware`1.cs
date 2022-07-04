using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Model;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fusap.Common.Mediator.Logging
{
    public class LoggingMiddleware<TRequest> : LoggingMiddlewareBase, IHandlerMiddleware<TRequest>
        where TRequest : IRequest
    {
        public LoggingMiddleware(ILogger<TRequest> logger) : base(logger)
        {
        }

        public async Task<IResult> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<IResult> next)
        {
            LogStart(request);
            try
            {
                var result = await next();

                LogFinished(request, result);

                return result;
            }
            catch (Exception ex)
            {
                LogException(request, ex);

                throw;
            }
        }
    }
}
