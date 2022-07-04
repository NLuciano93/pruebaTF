using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Fusap.Common.Authorization.Client
{
    [ExcludeFromCodeCoverage]
    public static class FusapAuthorizationClientExtensions
    {
        public static async Task<AuthorizationResult> AuthorizeAsync(this IFusapAuthorizationClient client,
            Requirement requirement, CancellationToken cancellationToken = default)
        {
            return await client.AuthorizeAsync(new[] { requirement }, cancellationToken);
        }

        public static Task<AuthorizationResult> AuthorizeAsync(this IFusapAuthorizationClient client,
            Uri identity, Uri resource, string action, CancellationToken cancellationToken = default)
        {
            return client.AuthorizeAsync(new Requirement(identity, resource, action), cancellationToken);
        }

        public static Task<AuthorizationResult> AuthorizeAsync(this IFusapAuthorizationClient client,
            Uri identity, Uri resource, string[] actions, CancellationToken cancellationToken = default)
        {
            return client.AuthorizeAsync(new Requirement(identity, resource, actions), cancellationToken);
        }

        public static Task<AuthorizationResult> AuthorizeAsync(this IFusapAuthorizationClient client,
            Uri identity, Uri[] resources, string action, CancellationToken cancellationToken = default)
        {
            return client.AuthorizeAsync(new Requirement(identity, resources, action), cancellationToken);
        }

        public static Task<AuthorizationResult> AuthorizeAsync(this IFusapAuthorizationClient client,
            Uri identity, Uri[] resources, string[] actions, CancellationToken cancellationToken = default)
        {
            return client.AuthorizeAsync(new Requirement(identity, resources, actions), cancellationToken);
        }
    }
}