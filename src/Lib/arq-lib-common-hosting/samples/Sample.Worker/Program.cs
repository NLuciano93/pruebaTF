using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Sample.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureFusapHost(options => options.ApplicationName = "sample-worker")
                .ConfigureFusapWorker()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    //...
                });
    }
}
