using System;
using System.Collections.Generic;
using Fusap.Common.Authorization.Client;
using Fusap.Common.Mediator;
using Fusap.Common.Mediator.Authorization;

namespace Fusap.Sample.Web.Requests.GetWeatherForecast
{
    public class WeatherAuthorizationRequirementsDescriptor : AuthorizationRequirementsDescriptor
    {
        public override IEnumerable<Requirement> DescribeRequirements(object request)
        {
            var actions = AuthorizeAttribute.ActionsFor(request.GetType());

            if (actions == null)
            {
                yield break;
            }

            yield return new Requirement(FusapResources.Person(Guid.NewGuid()),
                FusapResources.Account(Guid.NewGuid()), actions);
        }
    }
}
