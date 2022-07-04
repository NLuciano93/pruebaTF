using Fusap.Common.Mediator;
using Fusap.Common.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    public class GetWeatherTodayHandler : Handler<GetWeatherTodayRequest, WeatherForecast>
    {
        public override async Task<Result<WeatherForecast>> Handle(GetWeatherTodayRequest request, CancellationToken cancellationToken)
        {
            var rnd = new Random();
            var errorFactory = new TransactionDomainErrorFactory("FUN", "InternalTransfer");

            if (rnd.NextDouble() > 0.1)
            {


                var acc = new Account();

                var validationContext = new List<Error>();

                acc.ValidateA(acc.ValidateB(validationContext)).IsValid();
                acc.ValidateB(validationContext);

                if (validationContext.IsValid())
                {

                }


                //return new ValidationError(new[]
                //{
                //    new ValidationErrorDetail("propa", "aaa", "aaa"),
                //    new ValidationErrorDetail("propa", "bbb", "aaa"),
                //    new ValidationErrorDetail("propa", "ccc", "aaa"),
                //    new ValidationErrorDetail("propa", "bbb", "aaa")

                //});

                return new AggregatedDomainError(new DomainError[]
                {
                    errorFactory.AccountIsClosed(Guid.NewGuid()),
                    WeatherDomainError.DateNotAvailable(),
                    WeatherDomainError.DateNotAvailable(),
                    WeatherDomainError.DateNotAvailable()
                });

                //return new Error("a", "Failed");
            }

            if (rnd.NextDouble() > 0.1)
            {
                return Error(new Error("a", "b"));
            }

            if (rnd.NextDouble() > 0.1)
            {
                return NotFound(ErrorCatalog.ErrorWithOneParameter.Format("one"));
            }

            if (rnd.NextDouble() > 0.1)
            {
                return NotAuthorized(ErrorCatalog.ErrorWithTwoParameter.Format("one", "two"));
            }

            if (rnd.NextDouble() > 0.1)
            {
                return Invalid(ErrorCatalog.ErrorWithThreeParameter.Format("one", "two", "three"));
            }

            if (rnd.NextDouble() > 0.1)
            {
                return Success(new WeatherForecast());
            }

            return new WeatherForecast
            {
                Date = DateTime.Now,
                Summary = "Teste",
                TemperatureC = 33
            };
        }
    }
}