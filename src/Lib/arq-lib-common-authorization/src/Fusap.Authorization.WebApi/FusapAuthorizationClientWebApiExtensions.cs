using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Authorization.WebApi;

// ReSharper disable once CheckNamespace
namespace Fusap.Common.Authorization.Client
{
    public static class FusapAuthorizationClientWebApiExtensions
    {
        public static Task<AuthorizationResult> AuthorizeAsync(this IFusapAuthorizationClient client,
            ClaimsPrincipal identity, Uri resource, string action, CancellationToken cancellationToken = default)
        {
            return client.AuthorizeAsync(new Requirement(FusapResources.Person(identity.GetSubjectGuid()), resource, action), cancellationToken);
        }

        public static Task<AuthorizationResult> AuthorizeAsync(this IFusapAuthorizationClient client,
            ClaimsPrincipal identity, Uri resource, string[] actions, CancellationToken cancellationToken = default)
        {
            return client.AuthorizeAsync(new Requirement(FusapResources.Person(identity.GetSubjectGuid()), resource, actions), cancellationToken);
        }

        public static Task<AuthorizationResult> AuthorizeAsync(this IFusapAuthorizationClient client,
            ClaimsPrincipal identity, Uri[] resources, string action, CancellationToken cancellationToken = default)
        {
            return client.AuthorizeAsync(new Requirement(FusapResources.Person(identity.GetSubjectGuid()), resources, action), cancellationToken);
        }

        public static Task<AuthorizationResult> AuthorizeAsync(this IFusapAuthorizationClient client,
            ClaimsPrincipal identity, Uri[] resources, string[] actions, CancellationToken cancellationToken = default)
        {
            return client.AuthorizeAsync(new Requirement(FusapResources.Person(identity.GetSubjectGuid()), resources, actions), cancellationToken);
        }
    }
}