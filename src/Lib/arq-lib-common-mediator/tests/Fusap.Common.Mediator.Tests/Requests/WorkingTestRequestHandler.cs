using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Model;

namespace Fusap.Common.Mediator.Tests
{
    public class WorkingTestRequestHandler : Handler<TestRequest>
    {
        public override async Task<Result> Handle(TestRequest request, CancellationToken cancellationToken)
        {
            return Success();
        }
    }
}