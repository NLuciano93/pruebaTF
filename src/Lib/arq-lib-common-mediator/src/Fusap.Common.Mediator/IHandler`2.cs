using Fusap.Common.Model;
using MediatR;

namespace Fusap.Common.Mediator
{
    public interface IHandler<in TRequest, TValue> : IRequestHandler<TRequest, IResult<TValue>>
        where TRequest : IRequest<TValue>
    {
    }
}
