using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Fusap.Common.Model;
using Fusap.Common.Model.Presenter.WebApi;

namespace SampleWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : FusapApiController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();
            var ret = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(ret);
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("notfounderr")]
        public ActionResult GetNotFoundError()
        {
            return Ok(new NotFoundError("a", "b"));
        }

        [HttpGet("notfoundobj")]
        public ActionResult GetNotFoundObj()
        {
            return NotFound(new { campo = 1, campo2 = 2 });
        }

        [HttpGet("statuscode")]
        public IActionResult GetStatusCode()
        {
            return StatusCode(404);
        }

        [HttpGet("statuscode/{code}")]
        public IActionResult GetStatusCode2(int code)
        {
            return StatusCode(code);
        }

        [HttpGet("business")]
        public IActionResult GetBusiness()
        {
            return Ok(new BusinessRuleViolatedError(new BusinessRuleViolation("a", "b", new Dictionary<string, string>
            {
                {"accountId", "123"}
            })));
        }

        [HttpGet("unexpected")]
        public IActionResult GetUnexpected()
        {
            throw new InvalidCastException("Random cast");
        }
    }
}
