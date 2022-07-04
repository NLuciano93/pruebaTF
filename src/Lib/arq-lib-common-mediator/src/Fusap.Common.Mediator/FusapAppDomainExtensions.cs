using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class FusapAppDomainExtensions
    {
        public static Assembly[] GetFusapLoadedAssemblies(this AppDomain domain)
        {
            var fusapAssemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.FullName?.StartsWith("Fusap") == true)
                .ToArray();

            return fusapAssemblies;
        }
    }
}