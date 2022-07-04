using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Fusap.Common.Logger.LogTypes;

namespace SampleLogApp.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _controllerLogger;
        private readonly ILogger<SecurityLog> _securityLogger;

        public WeatherForecastController(ILogger<WeatherForecastController> controllerLogger, ILogger<SecurityLog> securityLogger)
        {
            _controllerLogger = controllerLogger;
            _securityLogger = securityLogger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get(bool emitError = false)
        {
            var position = new { Latitude = 25, Longitude = 134 };
            _securityLogger.LogInformation("Processed {position}", position);

            _controllerLogger.LogWarning("Some details here");

            var rng = new Random();
            if (rng.NextDouble() > 0.8)
            {
                throw new ApplicationException("As requested, here is your error :)");
            }

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}