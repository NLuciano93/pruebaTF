using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Fusap.Common.Model.Presenter.WebApi.Tests
{
    public class TestController : FusapApiController
    {
        public IPresenter GetPresenter()
        {
            return Presenter;
        }
    }

    public class PresenterConfigureDefaultOptionsTests
    {
        private PresenterOptions GetConfiguredOptions(bool? hideUnexpectedErrors = null)
        {
            var memoryOptions = new Dictionary<string, string>();
            if (hideUnexpectedErrors.HasValue)
            {
                memoryOptions.Add("Presenter:HideUnexpectedErrorDetails", hideUnexpectedErrors.Value.ToString());
            }

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(memoryOptions)
                .Build();
            var options = new PresenterOptions();

            var config = new PresenterConfigureDefaultOptions(configuration);
            config.Configure(options);

            return options;
        }

        [Fact]
        public void ErrorRenderer_WithConflictError_RendersCorrectly()
        {
            // Arrange
            var options = GetConfiguredOptions();

            // Act
            var result = options.ErrorRenderer!(new ConflictError("a", "b"));

            // Assert
            Assert.True(result is ConflictObjectResult);

            var apiError = (ApiError)((ObjectResult)result).Value;

            Assert.Equal(RootErrorCatalog.Conflict.Code, apiError.Code);
            Assert.Equal(RootErrorCatalog.Conflict.Message, apiError.Message);
            Assert.Equal("a", apiError.Errors.First().ErrorCode);
            Assert.Equal("b", apiError.Errors.First().Message);
        }

        [Fact]
        public void ErrorRenderer_WithNotFoundError_RendersCorrectly()
        {
            // Arrange
            var options = GetConfiguredOptions();

            // Act
            var result = options.ErrorRenderer!(new NotFoundError("a", "b"));

            // Assert
            Assert.True(result is NotFoundObjectResult);

            var apiError = (ApiError)((ObjectResult)result).Value;

            Assert.Equal(RootErrorCatalog.NotFound.Code, apiError.Code);
            Assert.Equal(RootErrorCatalog.NotFound.Message, apiError.Message);
            Assert.Equal("a", apiError.Errors.First().ErrorCode);
            Assert.Equal("b", apiError.Errors.First().Message);
        }

        [Fact]
        public void ErrorRenderer_WithValidationError_RendersCorrectly()
        {
            // Arrange
            var options = GetConfiguredOptions();

            // Act
            var result = options.ErrorRenderer!(new ValidationError(new ValidationErrorDetail("prop", "a", "b")));

            // Assert
            Assert.True(result is BadRequestObjectResult);

            var apiError = (ApiError)((ObjectResult)result).Value;

            Assert.Equal(RootErrorCatalog.Invalid.Code, apiError.Code);
            Assert.Equal(RootErrorCatalog.Invalid.Message, apiError.Message);
            Assert.Equal("prop", apiError.Errors.First().Path);
            Assert.Equal("a", apiError.Errors.First().ErrorCode);
            Assert.Equal("b", apiError.Errors.First().Message);
        }

        [Fact]
        public void ErrorRenderer_WithBusinessRuleViolatedError_RendersCorrectly()
        {
            // Arrange
            var options = GetConfiguredOptions();

            // Act
            var result = options.ErrorRenderer!(new BusinessRuleViolatedError(new BusinessRuleViolation("a", "b")));

            // Assert
            Assert.True(result is UnprocessableEntityObjectResult);

            var apiError = (ApiError)((ObjectResult)result).Value;

            Assert.Equal(RootErrorCatalog.BusinessRuleViolated.Code, apiError.Code);
            Assert.Equal(RootErrorCatalog.BusinessRuleViolated.Message, apiError.Message);
            Assert.Equal("a", apiError.Errors.First().ErrorCode);
            Assert.Equal("b", apiError.Errors.First().Message);
        }

        [Fact]
        public void ErrorRenderer_WithNotAuthorizedError_RendersCorrectly()
        {
            // Arrange
            var options = GetConfiguredOptions();

            // Act
            var result = options.ErrorRenderer!(new NotAuthorizedError("a", "b"));

            // Assert
            Assert.True(result is UnauthorizedObjectResult);

            var apiError = (ApiError)((ObjectResult)result).Value;

            Assert.Equal(RootErrorCatalog.NotAuthorized.Code, apiError.Code);
            Assert.Equal(RootErrorCatalog.NotAuthorized.Message, apiError.Message);
            Assert.Equal("a", apiError.Errors.First().ErrorCode);
            Assert.Equal("b", apiError.Errors.First().Message);
        }

        [Fact]
        public void ErrorRenderer_WithUnexpectedErrorAndNoDetails_RendersCorrectly()
        {
            // Arrange
            var options = GetConfiguredOptions();

            // Act
            var result = options.ErrorRenderer!(UnexpectedError.FromException(new Exception()));

            // Assert
            Assert.True(result is ObjectResult);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);

            var apiError = (ApiError)((ObjectResult)result).Value;

            Assert.Equal(RootErrorCatalog.UnexpectedError.Code, apiError.Code);
            Assert.Equal(RootErrorCatalog.UnexpectedError.Message, apiError.Message);
            Assert.Equal("UNEXPECTED_ERROR", apiError.Errors.First().ErrorCode);
            Assert.Equal("An unexpected error happened", apiError.Errors.First().Message);
        }

        [Fact]
        public void ErrorRenderer_WithUnexpectedErrorAndWithDetails_RendersCorrectly()
        {
            // Arrange
            var options = GetConfiguredOptions(false);

            // Act
            var result = options.ErrorRenderer!(UnexpectedError.FromException(new Exception()));

            // Assert
            Assert.True(result is ObjectResult);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);

            var apiError = (ApiError)((ObjectResult)result).Value;

            Assert.Equal(RootErrorCatalog.UnexpectedError.Code, apiError.Code);
            Assert.Equal(RootErrorCatalog.UnexpectedError.Message, apiError.Message);
            Assert.Equal("UNEXPECTED_ERROR", apiError.Errors.First().ErrorCode);
            Assert.Equal("System.Exception: Exception of type 'System.Exception' was thrown.", apiError.Errors.First().Message);
        }
    }
}
