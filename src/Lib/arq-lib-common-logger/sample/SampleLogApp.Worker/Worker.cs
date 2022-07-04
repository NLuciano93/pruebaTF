using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace SampleLogApp.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfigurationRoot _configurationRoot;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configurationRoot = (IConfigurationRoot)configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var rnd = new Random();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                Log.Logger.Information("Static logging at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                if (rnd.NextDouble() > 0.75)
                {
                    _configurationRoot.Reload();
                }
            }
        }
    }
}
