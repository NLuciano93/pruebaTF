using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Fusap.Common.Model.Presenter.WebApi
{
    public class PresenterConfigureDefaultOptions : IConfigureOptions<PresenterOptions>
    {
        private readonly IConfiguration _configuration;

        public PresenterConfigureDefaultOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(PresenterOptions options)
        {
            var hideUnexpectedErrorDetails = _configuration.GetValue("Presenter:HideUnexpectedErrorDetails", true);

            options
                .WithErrorRenderer<ConflictError>(error => new ConflictObjectResult(new ApiError
                {
                    Code = RootErrorCatalog.Conflict.Code,
                    Message = RootErrorCatalog.Conflict.Message,
                    Errors = new[] { new ApiErrorDetail
                    {
                        ErrorCode = error.Code,
                        Message = error.Message,
                    }}
                }))
                .WithErrorRenderer<NotFoundError>(error => new NotFoundObjectResult(new ApiError
                {
                    Code = RootErrorCatalog.NotFound.Code,
                    Message = RootErrorCatalog.NotFound.Message,
                    Errors = new[] { new ApiErrorDetail
                    {
                        ErrorCode = error.Code,
                        Message = error.Message,
                    }}
                }))
                .WithErrorRenderer<ValidationError>(error => new BadRequestObjectResult(new ApiError
                {
                    Code = RootErrorCatalog.Invalid.Code,
                    Message = RootErrorCatalog.Invalid.Message,
                    Errors = error.Details.Select(field => new ApiErrorDetail
                    {
                        Path = field.PropertyName,
                        ErrorCode = field.Code,
                        Message = field.Message,
                    })
                }))
                .WithErrorRenderer<BusinessRuleViolatedError>(error => new UnprocessableEntityObjectResult(new ApiError
                {
                    Code = RootErrorCatalog.BusinessRuleViolated.Code,
                    Message = RootErrorCatalog.BusinessRuleViolated.Message,
                    Errors = error.Violations.Select(field => new ApiErrorDetail
                    {
                        Properties = field.Properties?.Select(x => new ApiErrorDetailProperty(x)),
                        ErrorCode = field.Code,
                        Message = field.Message,
                    })
                }))
                .WithErrorRenderer<NotAuthorizedError>(error => new UnauthorizedObjectResult(new ApiError
                {
                    Code = RootErrorCatalog.NotAuthorized.Code,
                    Message = RootErrorCatalog.NotAuthorized.Message,
                    Errors = new[] { new ApiErrorDetail
                    {
                        ErrorCode = error.Code,
                        Message = error.Message,
                    }}
                }))
                .WithErrorRenderer<UnexpectedError>(error => new ObjectResult(new ApiError
                {
                    Code = RootErrorCatalog.UnexpectedError.Code,
                    Message = RootErrorCatalog.UnexpectedError.Message,
                    Errors = new[] { new ApiErrorDetail
                    {
                        ErrorCode = error.Code,
                        Message = (!hideUnexpectedErrorDetails ? error.Exception?.ToString() : null)
                                  ?? error.Message,
                    }}
                })
                { StatusCode = StatusCodes.Status500InternalServerError })
                ;
        }
    }
}
