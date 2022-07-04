using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Fusap.Common.Mediator
{
    public interface IFusapMediatorBuilder
    {
        public IServiceCollection Services { get; }
        public Assembly[] Assemblies { get; }
    }
}
