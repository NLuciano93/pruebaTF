using Fusap.Common.Model;

namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    public class DomainError : Error
    {
        public DomainError(string code, string message) : base(code, message)
        {
        }
    }
}