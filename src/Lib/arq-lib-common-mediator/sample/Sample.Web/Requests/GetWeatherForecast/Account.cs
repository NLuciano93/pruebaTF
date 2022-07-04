using Fusap.Common.Model;
using System.Collections.Generic;

namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    public class Account
    {
        public decimal Balance { get; set; }

        public IList<Error> ValidateA(IList<Error> errors)
        {
            if (Balance < 0)
            {
                errors.Add(TransactionDomainError.AccountIsClosed("a", "a"));
            }
            return errors;
        }

        public IList<Error> ValidateB(IList<Error> errors)
        {
            if (Balance < 0)
            {
                errors.Add(TransactionDomainError.AccountIsClosed("a", "a"));
            }

            return errors;
        }

        //public bool IsValid()
        //{
        //    var x = new List<Error>();

        //    ValidateA(x);

        //    return !x.Any();
        //}
    }
}