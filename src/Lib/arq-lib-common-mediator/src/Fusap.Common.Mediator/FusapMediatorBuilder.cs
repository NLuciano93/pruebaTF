using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Fusap.Common.Mediator
{
    internal class FusapMediatorBuilder : IFusapMediatorBuilder
    {
        public IServiceCollection Services { get; }
        public Assembly[] Assemblies { get; }

        public FusapMediatorBuilder(IServiceCollection services, Assembly[] assemblies)
        {
            Services = services;
            Assemblies = assemblies;
        }
    }
}
