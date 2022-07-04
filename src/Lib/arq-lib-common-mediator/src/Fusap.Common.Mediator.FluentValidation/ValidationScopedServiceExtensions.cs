using System;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace FluentValidation
{
    public static class ValidationScopedServiceExtensions
    {
        public static object GetRequiredScopedService(this IServiceProvider provider, Type type)
        {
            return provider.CreateScope().ServiceProvider.GetRequiredService(type);
        }

        public static T GetRequiredScopedService<T>(this IServiceProvider provider)
        {
            return provider.CreateScope().ServiceProvider.GetRequiredService<T>();
        }
    }
}
