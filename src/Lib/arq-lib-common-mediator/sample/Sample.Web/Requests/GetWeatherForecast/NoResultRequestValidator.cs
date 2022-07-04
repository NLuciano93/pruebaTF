using FluentValidation;

namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    public class NoResultRequestValidator : AbstractValidator<NoResultRequest>
    {
        public NoResultRequestValidator()
        {
            RuleFor(x => x.RandomA)
                .GreaterThan(10)
                .LessThanOrEqualTo(20)
                .WithErrorCode("AAA");
        }
    }
}