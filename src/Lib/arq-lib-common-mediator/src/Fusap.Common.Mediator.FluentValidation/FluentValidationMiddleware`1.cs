using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Fusap.Common.Model;
using MediatR;

namespace Fusap.Common.Mediator.FluentValidation
{
    public class FluentValidationMiddleware<TRequest> : FluentValidationMiddlewareBase<TRequest>, IHandlerMiddleware<TRequest> where TRequest : IRequest
    {
        public FluentValidationMiddleware(IEnumerable<IValidationContextFilter> contextFilters,
            IEnumerable<IValidator<TRequest>> validators
            ) : base(contextFilters, validators)
        {
        }

        public async Task<IResult> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<IResult> next)
        {
            var error = await ValidateAsync(request, cancellationToken);

            if (error != null)
            {
                return new Result(error);
            }

            return await next();
        }
    }
}
