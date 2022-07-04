using System.Linq;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Fusap.Common.Mediator;
using Fusap.Common.Mediator.Authorization;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthorizationExtensions
    {
        public static IFusapMediatorBuilder UseAuthorization(this IFusapMediatorBuilder builder)
        {
            builder.Services.AddFusapAuthorizationClient();

            builder.AddGenericMiddleware(typeof(AuthorizationMiddleware<>));
            builder.AddGenericMiddleware(typeof(AuthorizationMiddleware<,>));

            var requirementDescriptors = builder.Assemblies
                .SelectMany(x => x.GetTypes())
                .Where(t => typeof(IAuthorizationRequirementsDescriptor).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract && t.IsClass && !t.IsGenericTypeDefinition)
                .Select(t => new ServiceDescriptor(typeof(IAuthorizationRequirementsDescriptor), t, ServiceLifetime.Scoped))
                ;

            builder.Services.TryAddEnumerable(requirementDescriptors);

            return builder;
        }
    }
}
