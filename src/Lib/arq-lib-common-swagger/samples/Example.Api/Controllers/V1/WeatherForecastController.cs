using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Fusap.Common.Swagger;

namespace Example.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// Standard get weather endpoint
        /// </summary>
        /// <returns></returns>
        [ApiExposureLevel(ApiExposureLevel.Open)]
        [Obsolete]
        [HttpGet]
        public IEnumerable<EarthForecast> GetV1()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new EarthForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
                .ToArray();
        }

        /// <summary>
        /// A patch operation
        /// </summary>
        /// <returns></returns>
        [HttpPatch(Name = "custom-operation-id")]
        public IEnumerable<EarthForecast> Patch()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// A delete endpoint
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete(Name = "delete-override")]
        public IEnumerable<EarthForecast> Delete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// An obsolete endpoint
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Obsolete]
        public string ObsoleteAction()
        {
            return "done";
        }

        /// <summary>
        /// A paginated action
        /// </summary>
        /// <param name="pageNumber">The page number</param>
        /// <param name="pageSize">The page size</param>
        /// <returns></returns>
        [HttpGet("action-with-paginated-response")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesPaginationResponseHeaders]
        public IActionResult ActionWithPagination([FromHeader(Name = "X-pageNumber")] int pageNumber, [FromHeader(Name = "X-pageSize")] int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// An action with custom headers
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseHeader("x-test-number", StatusCodes.Status200OK, Description = "Test number header", Type = HeaderResponseType.Number)]
        [ProducesResponseHeader("x-test-string", StatusCodes.Status201Created, Description = "Test string header", Type = HeaderResponseType.String)]
        [HttpGet("action-with-custom-response-headers")]
        public IActionResult ActionWithCustomResponseHeaders()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// An action that is secured with OAuth2
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [ApiExposureLevel]
        [SecurityScheme(SecuritySchemeType.OAuth2)]
        [ProducesResponseType(typeof(Forecast[]), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("action-with-oauth2-scheme")]
        public IActionResult ActionSecuredWithOAuth2Scheme()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// An action that is secured with basic scheme
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("action-secured-with-basic-http-scheme")]
        [ProducesResponseType(typeof(Forecast[]), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SecurityScheme(SecuritySchemeType.Http)]
        public IActionResult ActionSecuredWithBasicHttpScheme()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// An action that is secured with api key
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("action-secured-with-api-key-scheme")]
        [ProducesResponseType(typeof(Forecast[]), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult ActionSecuredWithApiKeyScheme()
        {
            throw new NotImplementedException();
        }
    }
}
