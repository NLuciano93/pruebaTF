using Fusap.Common.Mediator;
using System;

namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    public class GetWeatherTodayRequest : Request<WeatherForecast>
    {
        public Guid AccountId { get; set; }
    }
}