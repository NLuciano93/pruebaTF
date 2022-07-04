using Fusap.Common.Model;
using MediatR;

namespace Fusap.Common.Mediator
{
    public interface IHandlerMiddleware<in TRequest, TValue> : IPipelineBehavior<TRequest, IResult<TValue>>
        where TRequest : IRequest<TValue>
    {
    }
}
