using Fusap.Common.Model;

namespace Fusap.Common.Mediator
{
    public interface IRequest<out TValue> : MediatR.IRequest<IResult<TValue>>
    {

    }
}
