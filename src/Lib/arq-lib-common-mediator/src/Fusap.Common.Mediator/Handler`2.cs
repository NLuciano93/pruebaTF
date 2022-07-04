using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Model;
using MediatR;

namespace Fusap.Common.Mediator
{
    public abstract class Handler<TRequest, TValue> : HandlerBase, IHandler<TRequest, TValue>
        where TRequest : Request<TValue>
    {
        public abstract Task<Result<TValue>> Handle(TRequest request, CancellationToken cancellationToken);

        async Task<IResult<TValue>> IRequestHandler<TRequest, IResult<TValue>>.Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await Handle(request, cancellationToken);
            }
            catch (Exception exception)
            {
                return new Result<TValue>(HandleException(exception));
            }
        }

        protected static Result<TValue> Success(TValue value)
        {
            return Result.Success(value);
        }
    }
}
