using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Model;

namespace Fusap.Common.Mediator.Tests
{
    public class FaultyTestRequestHandler : Handler<TestRequest>
    {
        public override Task<Result> Handle(TestRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}