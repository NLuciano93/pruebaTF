using Fusap.Common.Mediator;
using Fusap.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    public class GetWeatherForecastHandler : Handler<GetWeatherForecastRequest, IEnumerable<WeatherForecast>>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public override async Task<Result<IEnumerable<WeatherForecast>>> Handle(GetWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            var rng = new Random();

            await Task.Delay(rng.Next(100, 300), cancellationToken);

            var data = Enumerable
                .Range(1, 5)
                .Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();


            if (rng.NextDouble() > 0.9)
            {
                // Implicit cast from Error -> Result
                return new Error("aaa", "assaas");
            }

            if (rng.NextDouble() > 0.9)
            {
                return Result.Failure(new Error("aaa", "assaas"));
            }

            if (rng.NextDouble() > 0.9)
            {
                return new Result<IEnumerable<WeatherForecast>>(data.AsEnumerable());
            }


            // Explicit success
            return Result.Success(data.AsEnumerable());
        }
    }
}