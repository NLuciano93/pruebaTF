using System.Collections.Generic;
using Fusap.Common.Authorization.Client;

namespace Fusap.Common.Mediator.Authorization
{
    public abstract class AuthorizationRequirementsDescriptor<TRequest> : AuthorizationRequirementsDescriptor
    {
        public override IEnumerable<Requirement> DescribeRequirements(object request)
        {
            if (!(request is TRequest tRequest))
            {
                return NoRequirements;
            }

            return DescribeRequirements(tRequest);
        }

        protected abstract IEnumerable<Requirement> DescribeRequirements(TRequest request);
    }
}
