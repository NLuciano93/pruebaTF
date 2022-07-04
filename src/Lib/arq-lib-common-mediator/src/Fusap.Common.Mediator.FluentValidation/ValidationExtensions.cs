using System.Linq;
using System.Reflection;
using FluentValidation;
using Fusap.Common.Mediator;
using Fusap.Common.Mediator.FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ValidationExtensions
    {
        public static IFusapMediatorBuilder UseValidation(this IFusapMediatorBuilder builder)
        {
            builder.Services.AddValidatorsFromAssemblies(builder.Assemblies);

            builder.AddGenericMiddleware(typeof(FluentValidationMiddleware<>));
            builder.AddGenericMiddleware(typeof(FluentValidationMiddleware<,>));

            var contextFilters = builder.Assemblies
                    .SelectMany(x => x.GetTypes())
                    .Where(t => typeof(IValidationContextFilter).IsAssignableFrom(t))
                    .Where(t => !t.IsAbstract && t.IsClass && !t.IsGenericTypeDefinition)
                    .Select(t => new ServiceDescriptor(typeof(IValidationContextFilter), t, ServiceLifetime.Transient))
                ;

            builder.Services.TryAddEnumerable(contextFilters);

            return builder;
        }

        public static IServiceCollection AddValidatorForInterface<TValidator, TInterface>(
            this IServiceCollection services, Assembly[] assemblies) where TValidator : IValidator<TInterface>
        {
            var requestTypes = assemblies
                    .SelectMany(x => x.GetTypes())
                    .Where(t => !t.IsInterface && typeof(TInterface).IsAssignableFrom(t))
                ;

            var descriptors = requestTypes
                .Select(t =>
                    new ServiceDescriptor(typeof(IValidator<>).MakeGenericType(t), typeof(TValidator), ServiceLifetime.Scoped));

            services.TryAddEnumerable(descriptors);

            return services;
        }
    }
}
