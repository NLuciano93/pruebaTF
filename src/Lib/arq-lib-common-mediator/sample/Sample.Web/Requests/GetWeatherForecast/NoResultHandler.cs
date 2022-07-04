using Fusap.Common.Mediator;
using Fusap.Common.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    public class NoResultHandler : Handler<NoResultRequest>
    {
        public override Task<Result> Handle(NoResultRequest request, CancellationToken cancellationToken)
        {
            var r = new Random();

            return NotAuthorized(ErrorCatalog.ErrorWithTwoParameter.Format("teste", "teste2"));

            if (r.NextDouble() > 0.6)
            {
                return Result.Failure(ErrorCatalog.ErrorWithThreeParameter.Format(1, 2, 3));
            }

            return Result.Success();
        }
    }
}