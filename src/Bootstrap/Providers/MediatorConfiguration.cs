using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Fusap.TimeSheet.Bootstrap.Providers
{
    public static class MediatorConfiguration
    {
        public static IServiceCollection ConfigureMediatrServices(this IServiceCollection services)
        {
            Assembly.Load("Fusap.TimeSheet.Application");
            Assembly.Load("Fusap.TimeSheet.Api");

            var fusapAssemblies = AppDomain.CurrentDomain.GetFusapLoadedAssemblies();

            services.AddAutoMapper(fusapAssemblies);

            try
            {
                services.AddFusapMediator(fusapAssemblies, opt => opt
                .UseValidation()
                );
            }
            catch (ReflectionTypeLoadException ex)
            {
                var sb = new StringBuilder();

                if (ex?.LoaderExceptions != null)
                {
                    foreach (var exSub in ex.LoaderExceptions)
                    {
                        sb.AppendLine(exSub?.Message);

                        if (exSub is FileNotFoundException exFileNotFound && !string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }

                        sb.AppendLine();
                    }
                }

                var errorMessage = sb.ToString();
            }

            return services;
        }
    }
}
