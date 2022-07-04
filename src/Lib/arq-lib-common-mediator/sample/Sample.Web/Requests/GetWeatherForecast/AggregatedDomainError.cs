using Fusap.Common.Model;
using System.Collections.Generic;

namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    public class AggregatedDomainError : Error
    {
        public IEnumerable<DomainError> Errors { get; set; }

        public AggregatedDomainError(IEnumerable<DomainError> errors) : base("DOMAIN_ERROR", "aaaa")
        {
            Errors = errors;
        }
    }
}