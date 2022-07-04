using Fusap.Common.Mediator;
using System.Collections.Generic;

namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    [Authorize("test-permission")]
    public class GetWeatherForecastRequest : Request<IEnumerable<WeatherForecast>>
    {
        public int RandomB { get; set; }
    }
}