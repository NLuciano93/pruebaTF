using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Fusap.Common.Model.ErrorCatalogs;

namespace Fusap.Sample.Web
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.Write(ErrorCatalogDescription.For(typeof(ErrorCatalog)).SerializeAsMarkdown());

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureFusapHost(options => options.ApplicationName = "test")
                .ConfigureFusapWebApi<Startup>(apiInfo =>
                {
                    apiInfo.Title = "Test Service";
                    apiInfo.Description = "REST Api Test";
                });
        }
    }
}
