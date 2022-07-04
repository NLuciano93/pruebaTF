using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Fusap.Common.Mediator.FluentValidation;

namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    public class GetWeatherForecastValidator : AbstractValidator<GetWeatherForecastRequest>
    {
        private static Random Rng = new Random();
        public GetWeatherForecastValidator()
        {
            RuleFor(x => x.RandomB)
                .GreaterThan(Rng.Next(0, 30))
                .LessThanOrEqualTo(Rng.Next(20, 50))
                .WithErrorCatalog(ErrorCatalog.ErrorWithOneParameter, "Literal")
                ;

            RuleFor(x => x.RandomB)
                .Must((request, i, arg3) =>
                {
                    return i > (int)arg3.RootContextData["test"] + Rng.Next(-100, 100);
                });
        }
    }

    public class RandomValidationContextFilter : IValidationContextFilter
    {
        public Task ApplyAsync(IValidationContext context, CancellationToken cancellationToken)
        {
            context.RootContextData["test"] = (new Random()).Next(0, 100);

            return Task.CompletedTask;
        }
    }

    public class Random2ValidationContextFilter : IValidationContextFilter
    {
        public Task ApplyAsync(IValidationContext context, CancellationToken cancellationToken)
        {
            context.RootContextData["test2"] = (new Random()).Next(0, 100);

            return Task.CompletedTask;
        }
    }
}
