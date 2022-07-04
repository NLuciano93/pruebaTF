namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    public class WeatherDomainError : DomainError
    {
        private WeatherDomainError(string code, string message) : base(code, message)
        {
        }

        public static WeatherDomainError DateNotAvailable()
        {
            return new WeatherDomainError("123", "The date requested is not available");
        }

    }
}