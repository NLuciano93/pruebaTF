using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fusap.Common.Model;
using Fusap.Common.Model.Presenter.WebApi;
using Fusap.Sample.Web.Requests.GetWeatherForecast;

namespace Fusap.Sample.Web.Controllers
{
    [ApiVersion("2")]
    [Route("[Controller]")]
    public class WeatherForecastController : FusapApiController
    {
        [HttpGet("set")]
        public ActionResult<Pagination<WeatherForecast>> GetSet()
        {
            return NotFound();

            //var ret = GetItems();

            //foreach (var x in ret.Value)
            //{
            //    Console.WriteLine(x.Summary);
            //}

            //return Ok(ret);
        }

        public Result<Pagination<WeatherForecast>> GetItems()
        {
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var rng = new Random();

            var data = Enumerable
                .Range(1, 5)
                .Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = summaries[rng.Next(summaries.Length)]
                })
                .ToPagination(123);

            return Result.Success(data);
        }



        [HttpGet("err1")]
        public ActionResult GetError1(string id)
        {
            return Error(
                new BusinessRuleViolatedError(
                    ErrorCatalog.ErrorWithOneParameter.Format(1),
                    ErrorCatalog.ErrorWithTwoParameter.Format(1, 2),
                    ErrorCatalog.ErrorWithTwoParameter.Format(1, 2, 3),
                    new BusinessRuleViolation(ErrorCatalog.ErrorWithZeroParameters, new Dictionary<string, string>()
                    {
                        { "AccountId", "123" },
                        { "Balance", "555" }
                    })
                ));
        }

        [HttpGet("err2")]
        public ActionResult GetError2()
        {
            return Error(
                new ValidationError(
                    ErrorCatalog.ErrorWithOneParameter.Format(1),
                    ErrorCatalog.ErrorWithTwoParameter.Format(1, 2),
                    ErrorCatalog.ErrorWithTwoParameter.Format(1, 2, 3),
                    new ValidationErrorDetail("fieldBla", ErrorCatalog.ErrorWithZeroParameters)
                ));
        }

        [HttpGet("err3")]
        public ActionResult GetError3()
        {
            return Error(
                new NotAuthorizedError(ErrorCatalog.ErrorWithOneParameter.Format(1)));
        }

        [HttpGet("err4")]
        public ActionResult GetError4()
        {
            return Error(
                new NotFoundError(ErrorCatalog.ErrorWithOneParameter.Format(1)));
        }

        [HttpGet("err5")]
        public ActionResult GetError5()
        {
            return Error(
                UnexpectedError.FromException(new Exception()));
        }

        [HttpGet("err6")]
        public ActionResult GetError6()
        {
            return Error(
                new BusinessRuleViolatedError(new BusinessRuleViolation(ErrorCatalog.Test, new[]
                {
                    new KeyValuePair<string, string>("accountId", "123"),
                    new KeyValuePair<string, string>("balance", "5000"),
                })));
        }

        [HttpGet("errExp")]
        public ActionResult GetErrorExp()
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
        {
            var response = await Send(new GetWeatherForecastRequest
            {
                RandomB = new Random().Next(0, 50)
            });

            return Ok(response);
        }

        [HttpGet("2")]
        public async Task<ActionResult<WeatherForecast>> Teste()
        {
            var response2 = await Send(new NoResultRequest()
            {
                RandomA = new Random().Next(-100, 30)
            });

            return Ok(response2);
        }

        [HttpGet("3")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Teste2()
        {
            var response = Result.Success(new[] { new WeatherForecast() }.AsEnumerable());

            var ret = new ObjectResult(response);
            ret.StatusCode = 205;
            return ret;
        }
    }

    [ApiVersion("2")]
    [Route("{legalPersonId}/[Controller]")]
    //[Authorize]
    public class AssociationsController : FusapApiController
    {
        [HttpPost]
        public Task<ActionResult<object>> CreateSingleAssociationAsync(Guid legalPersonId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("batch")]
        public Task<ActionResult<object>> CreateBatchAssociationsAsync(Guid legalPersonId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Task<ActionResult<Pagination<WeatherForecast>>> SearchAssociationsAsync(Guid legalPersonId,
            [FromQuery] bool? hasAccount, [FromQuery] bool? isValid,
            [FromQuery] Guid? batchId, [FromQuery] string nameContains, [FromQuery] string documentType, [FromQuery] string documentNumberContains
        )
        {
            throw new NotImplementedException();
        }

        [HttpGet("{associationId}")]
        public Task<ActionResult<Pagination<WeatherForecast>>> GetAssociationAsync(Guid legalPersonId, Guid associationId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{associationId}")]
        public Task<ActionResult> RemoveAssociationAsync(Guid legalPersonId, Guid associationId)
        {
            throw new NotImplementedException();
        }
    }

}
