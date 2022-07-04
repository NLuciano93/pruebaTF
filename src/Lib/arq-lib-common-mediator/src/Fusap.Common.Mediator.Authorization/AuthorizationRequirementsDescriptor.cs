using System;
using System.Collections.Generic;
using Fusap.Common.Authorization.Client;

namespace Fusap.Common.Mediator.Authorization
{
    public abstract class AuthorizationRequirementsDescriptor : IAuthorizationRequirementsDescriptor
    {
        protected IEnumerable<Requirement> NoRequirements => Array.Empty<Requirement>();

        public abstract IEnumerable<Requirement> DescribeRequirements(object request);
    }
}
