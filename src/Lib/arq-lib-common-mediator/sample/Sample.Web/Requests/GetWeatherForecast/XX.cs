using Fusap.Common.Model;
using System.Collections.Generic;
using System.Linq;

namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    public static class XX
    {
        public static bool IsValid(this IEnumerable<Error> ctx)
        {
            return !ctx.Any();
        }
    }
}