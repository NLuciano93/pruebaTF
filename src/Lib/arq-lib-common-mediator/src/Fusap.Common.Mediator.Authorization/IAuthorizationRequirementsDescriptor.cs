using System.Collections.Generic;
using Fusap.Common.Authorization.Client;

namespace Fusap.Common.Mediator.Authorization
{
    public interface IAuthorizationRequirementsDescriptor
    {
        IEnumerable<Requirement> DescribeRequirements(object request);
    }
}
