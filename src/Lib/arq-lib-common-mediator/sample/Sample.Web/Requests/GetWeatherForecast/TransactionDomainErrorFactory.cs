using System;

namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    public class TransactionDomainErrorFactory
    {
        public string Service { get; set; }
        public string Subdomain { get; set; }

        public TransactionDomainErrorFactory(string subdomain, string service)
        {
            Subdomain = subdomain;
            Service = service;
        }

        public TransactionDomainError AccountIsClosed(Guid accountId)
        {
            return new TransactionDomainError($"TRN-{Subdomain}-{Service}-1231", $"The account {accountId} is closed");
        }
    }
}