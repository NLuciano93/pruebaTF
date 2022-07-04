using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Sample.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureFusapHost(options => options.ApplicationName = "sample-web")
                .ConfigureFusapWebApi<Startup>(apiInfo =>
                {
                    apiInfo.Title = "Fusap Api";
                    apiInfo.Description = "Fusap host test";
                });
        }
    }
}
