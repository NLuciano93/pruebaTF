using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Fusap.Common.Model;
using MediatR;

namespace Fusap.Common.Mediator.FluentValidation
{
    public class FluentValidationMiddleware<TRequest, TData> :
        FluentValidationMiddlewareBase<TRequest>, IHandlerMiddleware<TRequest, TData> where TRequest : IRequest<TData>
    {
        public FluentValidationMiddleware(IEnumerable<IValidationContextFilter> contextFilters,
            IEnumerable<IValidator<TRequest>> validators
            ) : base(contextFilters, validators)
        {
        }

        public async Task<IResult<TData>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<IResult<TData>> next)
        {
            var error = await ValidateAsync(request, cancellationToken);

            if (error != null)
            {
                return new Result<TData>(error);
            }

            return await next();
        }
    }
}
