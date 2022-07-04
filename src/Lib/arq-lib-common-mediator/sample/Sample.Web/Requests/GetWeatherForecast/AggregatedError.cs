using Fusap.Common.Model;
using System.Collections.Generic;

namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    public class AggregatedError : Error
    {
        public IEnumerable<Error> Errors { get; set; }

        public AggregatedError(string code, string message) : base(code, message)
        {
        }
    }
}