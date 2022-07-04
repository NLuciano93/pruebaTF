using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Fusap.Common.Model;

namespace Fusap.Common.Mediator.FluentValidation
{
    public static class FluentValidatorExtensions
    {
        public static ValidationResult Validate<T>(
            this IEnumerable<IValidator<T>> validators, T target)
        {
            var context = new ValidationContext<T>(target);

            return validators.Validate(context);
        }

        public static ValidationResult Validate<T>(
            this IEnumerable<IValidator<T>> validators, ValidationContext<T> context)
        {
            ValidationResult? lastResult = null;

            foreach (var validator in validators)
            {
                lastResult = validator.Validate(context);
            }

            return lastResult ?? new ValidationResult();
        }

        public static Task<ValidationResult> ValidateAsync<T>(
            this IEnumerable<IValidator<T>> validators, T target, CancellationToken cancellationToken = default)
        {
            var context = new ValidationContext<T>(target);

            return validators.ValidateAsync(context, cancellationToken);
        }

        public static async Task<ValidationResult> ValidateAsync<T>(
            this IEnumerable<IValidator<T>> validators, ValidationContext<T> context, CancellationToken cancellationToken = default)
        {
            ValidationResult? lastResult = null;

            foreach (var validator in validators)
            {
                lastResult = await validator.ValidateAsync(context, cancellationToken);
            }

            return lastResult ?? new ValidationResult();
        }

        public static ValidationError AsFusapError(this IEnumerable<ValidationFailure> validationFailures)
        {
            return new ValidationError(validationFailures
                .Select(x => new ValidationErrorDetail(x.PropertyName, x.ErrorCode, x.ErrorMessage))
                .ToArray()
            );
        }
    }
}
