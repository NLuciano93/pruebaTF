using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SampleLogApp.Web
{
    public class VerboseBackgroundService : BackgroundService
    {
        private readonly ILogger<VerboseBackgroundService> _logger;

        public VerboseBackgroundService(ILogger<VerboseBackgroundService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var rng = new Random();

            var colors = new[]
            {
                "brown",
                "blue",
                "green",
                "black",
                "white",
                "red",
                "pink",
            };
            var animals = new[]
            {
                "fox",
                "dog",
                "sheep",
                "cat",
                "hippo",
                "poney",
                "horse",
                "cow",
                "bull",
            };
            var adjectives = new[]
            {
                "lazy",
                "calm",
                "agitated",
                "friendly",
                "mad",
                "angry",
                "young",
                "old",
                "crazy"
            };

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(500, stoppingToken);

                var color = colors[rng.Next(colors.Length)];
                var firstAnimal = animals[rng.Next(animals.Length)];
                var adjective = adjectives[rng.Next(adjectives.Length)];
                var secondAnimal = animals[rng.Next(animals.Length)];
                var numberOfTimes = rng.Next(2, 10);

                _logger.LogInformation("The quick {color} {firstAnimal} jumped over the {adjective} {secondAnimal} {numberOfTimes} times",
                    color, firstAnimal, adjective, secondAnimal, numberOfTimes);
            }
        }
    }
}