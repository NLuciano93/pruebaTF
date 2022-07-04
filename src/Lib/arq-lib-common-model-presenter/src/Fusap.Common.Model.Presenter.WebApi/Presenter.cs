using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fusap.Common.Model.Presenter.WebApi
{
    public class Presenter : IPresenter
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Presenter> _logger;
        private readonly PresenterOptions _options;
        private IMapper? _mapper;

        public Presenter(IServiceProvider serviceProvider, IOptions<PresenterOptions> options, ILogger<Presenter> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _options = options.Value;
        }

        public ActionResult Present(object? value, HttpStatusCode successStatusCode)
        {
            if (value is null)
            {
                return new StatusCodeResult((int)successStatusCode);
            }

            if (value is Error error)
            {
                return Present(error);
            }

            if (successStatusCode == HttpStatusCode.NoContent)
            {
                return new NoContentResult();
            }

            return new ObjectResult(value) { StatusCode = (int)successStatusCode };
        }

        public ActionResult Present(Error error)
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            _logger.LogWarning("Presenting error {PresentErrorDescription} ({PresentErrorType})",
                error.ToString(), error.GetType().Name);

            var rendered = _options.ErrorRenderer?.Invoke(error);

            if (rendered != null)
            {
                return rendered;
            }

            return new ObjectResult(new ApiError
            {
                Code = "Unexpected error",
                Message = "An unexpected error occurred",
                Errors = new[] { new ApiErrorDetail
                {
                    ErrorCode = error.Code,
                    Message = error.Message,
                }}
            })
            { StatusCode = StatusCodes.Status500InternalServerError };
        }

        public ActionResult Present(IResult result, HttpStatusCode successStatusCode)
        {
            if (!result.IsSuccessful())
            {
                return Present(result.Error!);
            }

            return Present(result.Value, successStatusCode);
        }

        public ActionResult<TValue> Present<TValue>(IResult<TValue> result, HttpStatusCode successStatusCode)
        {
            if (!result.IsSuccessful())
            {
                return Present(result.Error!);
            }

            return Present(result.Value, successStatusCode);
        }

        public ActionResult<TViewModel> PresentAs<TViewModel>(object? value, HttpStatusCode successStatusCode)
        {
            _mapper ??= _serviceProvider.GetRequiredService<IMapper>();

            var mappedValue = _mapper.Map<TViewModel>(value);

            return Present(mappedValue, successStatusCode);
        }

        public ActionResult<TViewModel> PresentAs<TViewModel>(IResult result, HttpStatusCode successStatusCode)
        {
            if (!result.IsSuccessful())
            {
                return Present(result.Error!);
            }

            _mapper ??= _serviceProvider.GetRequiredService<IMapper>();

            var mappedValue = _mapper.Map<TViewModel>(result.Value);

            return Present(mappedValue, successStatusCode);
        }
    }
}
