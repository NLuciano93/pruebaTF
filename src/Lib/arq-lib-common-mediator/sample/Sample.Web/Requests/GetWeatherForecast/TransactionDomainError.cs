namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    public class TransactionDomainError : DomainError
    {
        public TransactionDomainError(string code, string message) : base(code, message)
        {
        }

        public static TransactionDomainError Generic(string subdomain, string service, string code)
        {
            var message = code switch
            {
                "001" => "Tste",
                "002" => "Tste",
                "003" => "Tste",
                _ => "unknown"
            };

            return new TransactionDomainError($"TRN-{subdomain}-{service}-{code}", message);
        }

        public static TransactionDomainError AccountIsClosed(string subdomain, string service)
        {
            return new TransactionDomainError($"TRN-{subdomain}-{service}-1231", "Account is closed");
        }
    }
}