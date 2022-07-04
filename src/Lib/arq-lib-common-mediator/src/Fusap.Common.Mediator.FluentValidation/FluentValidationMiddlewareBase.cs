using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Fusap.Common.Model;

namespace Fusap.Common.Mediator.FluentValidation
{
    public abstract class FluentValidationMiddlewareBase<TRequest>
    {
        private readonly IEnumerable<IValidationContextFilter> _contextFilters;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        protected FluentValidationMiddlewareBase(
            IEnumerable<IValidationContextFilter> contextFilters,
            IEnumerable<IValidator<TRequest>> validators
        )
        {
            _contextFilters = contextFilters;
            _validators = validators;
        }

        protected async Task<Error?> ValidateAsync(TRequest request, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            foreach (var contextFilter in _contextFilters)
            {
                await contextFilter.ApplyAsync(context, cancellationToken);
            }

            var knownErrors = new List<ValidationFailure>();
            ValidationResult? lastResult = null;
            foreach (var validator in _validators)
            {
                lastResult = await validator.ValidateAsync(context, cancellationToken);

                var newErrors = lastResult.Errors.Where(e => !knownErrors.Contains(e));

                knownErrors.AddRange(newErrors);
            }

            if (lastResult == null || lastResult.IsValid)
            {
                return null;
            }

            return lastResult.Errors.AsFusapError();
        }
    }
}
