using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Model;

namespace Fusap.Common.Mediator.Tests
{
    public class FaultyTestRequestGuidHandler : Handler<TestRequestGuid, Guid>
    {
        public override Task<Result<Guid>> Handle(TestRequestGuid request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}