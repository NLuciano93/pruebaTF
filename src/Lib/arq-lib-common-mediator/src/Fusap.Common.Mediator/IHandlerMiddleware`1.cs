using Fusap.Common.Model;
using MediatR;

namespace Fusap.Common.Mediator
{
    public interface IHandlerMiddleware<in TRequest> : IPipelineBehavior<TRequest, IResult>
        where TRequest : IRequest
    {
    }
}
