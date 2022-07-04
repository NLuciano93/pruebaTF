using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenTracing;
using Fusap.Common.Authorization.Client;
using Fusap.Common.Model;

namespace Fusap.Common.Mediator.Authorization
{
    public sealed class AuthorizationMiddleware<TRequest> : AuthorizationMiddlewareBase,
        IHandlerMiddleware<TRequest> where TRequest : IRequest
    {
        public AuthorizationMiddleware(
            IFusapAuthorizationClient authorizationClient,
            IEnumerable<IAuthorizationRequirementsDescriptor> descriptors,
            ITracer tracer,
            ILogger<AuthorizationMiddleware<TRequest>> logger) :
            base(authorizationClient, descriptors, tracer, logger)
        {
        }

        public async Task<IResult> Handle(TRequest request, CancellationToken cancellationToken,
            MediatR.RequestHandlerDelegate<IResult> next)
        {
            if (!await ValidateAsync(request, cancellationToken))
            {
                return new Result(new NotAuthorizedError());
            }

            return await next();
        }
    }
}
