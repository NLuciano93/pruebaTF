using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Internal;
using FluentValidation.TestHelper;

// ReSharper disable once CheckNamespace
namespace FluentValidation
{
    public static class TestValidationExtensions
    {

        public static TestValidationResult<T> TestValidate<T>(
            this IValidator<T> validator, ValidationContext<T> contextToTest,
            Action<ValidationStrategy<T>>? options = null) where T : class
        {
            var validationResult = validator.Validate(contextToTest);
            return new TestValidationResult<T>(validationResult);
        }

        public static async Task<TestValidationResult<T>> TestValidateAsync<T>(
            this IValidator<T> validator, ValidationContext<T> contextToTest,
            Action<ValidationStrategy<T>>? options = null, CancellationToken cancellationToken = default) where T : class
        {
            var validationResult = await validator.ValidateAsync(contextToTest, cancellationToken);
            return new TestValidationResult<T>(validationResult);
        }
    }
}
