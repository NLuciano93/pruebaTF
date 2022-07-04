using Microsoft.Extensions.Hosting;

namespace Fusap.TimeSheet.Api
{
    public class Program
    {
        protected Program()
        {
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureFusapHost(options => options.ApplicationName = "fusap-timeSheet")
                .ConfigureFusapWebApi<Startup>(apiInfo =>
                {
                    apiInfo.Title = "Fusap.TimeSheet Service";
                    apiInfo.Description = "REST Api Fusap.TimeSheet Service";
                });
        }
    }
}
