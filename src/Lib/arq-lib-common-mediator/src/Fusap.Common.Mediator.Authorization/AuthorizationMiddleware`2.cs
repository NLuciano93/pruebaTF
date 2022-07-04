using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenTracing;
using Fusap.Common.Authorization.Client;
using Fusap.Common.Model;

namespace Fusap.Common.Mediator.Authorization
{
    public sealed class AuthorizationMiddleware<TRequest, TData> : AuthorizationMiddlewareBase,
        IHandlerMiddleware<TRequest, TData> where TRequest : IRequest<TData>
    {
        public AuthorizationMiddleware(
            IFusapAuthorizationClient authorizationClient,
            IEnumerable<IAuthorizationRequirementsDescriptor> descriptors,
            ITracer tracer,
            ILogger<AuthorizationMiddleware<TRequest, TData>> logger) :
            base(authorizationClient, descriptors, tracer, logger)
        {
        }

        public async Task<IResult<TData>> Handle(TRequest request, CancellationToken cancellationToken,
            MediatR.RequestHandlerDelegate<IResult<TData>> next)
        {
            if (!await ValidateAsync(request, cancellationToken))
            {
                return new Result<TData>(new NotAuthorizedError());
            }

            return await next();
        }
    }
}
