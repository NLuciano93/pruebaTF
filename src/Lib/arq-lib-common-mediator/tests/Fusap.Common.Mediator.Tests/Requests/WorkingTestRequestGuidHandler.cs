using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Model;

namespace Fusap.Common.Mediator.Tests
{
    public class WorkingTestRequestGuidHandler : Handler<TestRequestGuid, Guid>
    {
        public override Task<Result<Guid>> Handle(TestRequestGuid request, CancellationToken cancellationToken)
        {
            return Success(Guid.Empty);
        }
    }
}