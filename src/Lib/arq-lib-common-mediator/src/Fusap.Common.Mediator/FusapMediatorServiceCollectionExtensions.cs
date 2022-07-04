using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fusap.Common.Mediator;
using MediatR;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class FusapMediatorServiceCollectionExtensions
    {
        public static IServiceCollection AddFusapMediator(this IServiceCollection services, IEnumerable<Assembly> assemblies,
            Action<IFusapMediatorBuilder>? configure = null)
        {
            var arr = assemblies.ToArray();

            services.AddMediatR(arr);

            var builder = new FusapMediatorBuilder(services, arr);

            builder.UseLogging();

            configure?.Invoke(builder);

            return services;
        }
    }
}
