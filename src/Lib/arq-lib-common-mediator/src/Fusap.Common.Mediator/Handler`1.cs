using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Model;
using MediatR;

namespace Fusap.Common.Mediator
{
    public abstract class Handler<TRequest> : HandlerBase, IHandler<TRequest> where TRequest : Request
    {
        public abstract Task<Result> Handle(TRequest request, CancellationToken cancellationToken);

        async Task<IResult> IRequestHandler<TRequest, IResult>.Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await Handle(request, cancellationToken);
            }
            catch (Exception exception)
            {
                return Result.Failure(HandleException(exception));
            }
        }

        protected static Result Success()
        {
            return Result.Success();
        }
    }
}
