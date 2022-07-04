using System.Threading;
using System.Threading.Tasks;

namespace Fusap.Common.Authorization.Client
{
    public interface IFusapAuthorizationClient
    {
        Task<AuthorizationResult> AuthorizeAsync(Requirement[] requirements, CancellationToken cancellationToken = default);
    }
}