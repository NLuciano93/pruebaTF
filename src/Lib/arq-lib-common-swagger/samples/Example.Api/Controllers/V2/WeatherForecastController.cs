using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Fusap.Common.Swagger;

namespace Example.Api.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// Standard get weather endpoint
        /// </summary>
        /// <returns>
        /// </returns>
        [ApiExposureLevel(ApiExposureLevel.Open)]
        [HttpGet]
        [ProducesResponseType(typeof(Forecast[]), 200)]
        //[ProducesResponseType(typeof(EarthForecast), 200)]
        //[ProducesResponseType(typeof(MarsForecast), 200)]
        public ActionResult GetV2([FromQuery] WindDirection direction = WindDirection.South)
        {
            var rng = new Random();
            return Ok(Enumerable.Range(1, 5).Select(index => new EarthForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
                .ToArray());
        }

        /// <summary>
        /// Another action
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        [HttpPost("test")]
        public string OtherAction(MarsForecast test)
        {
            return "done";
        }
    }
}
