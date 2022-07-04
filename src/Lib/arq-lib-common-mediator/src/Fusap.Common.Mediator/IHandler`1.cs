using Fusap.Common.Model;
using MediatR;

namespace Fusap.Common.Mediator
{
    public interface IHandler<in TRequest> : IRequestHandler<TRequest, IResult>
        where TRequest : IRequest
    {
    }
}
