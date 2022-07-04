using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Fusap.Common.Model.Presenter.WebApi.Tests
{
    public class PresenterTests
    {
        private IServiceProvider EmptyServiceProvider()
        {
            return new ServiceCollection().BuildServiceProvider();
        }

        [Fact]
        public void Present_WithErrorNull_Throws()
        {
            // Arrange
            var sut = new Presenter(EmptyServiceProvider(), Options.Create(new PresenterOptions()),
                NullLogger<Presenter>.Instance);

            // Act
            void Act()
            {
                sut.Present(null!);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(Act);
        }

        [Fact]
        public void Present_WithErrorAndNoRenders_RendersDefaultErrorResponse()
        {
            // Arrange
            var error = UnexpectedError.FromException(new NotImplementedException());

            var sut = new Presenter(EmptyServiceProvider(), Options.Create(new PresenterOptions()),
                NullLogger<Presenter>.Instance);

            // Act
            var result = sut.Present(error);

            // Assert
            Assert.True(result is ObjectResult);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public void Present_WithErrorAndRender_ReturnsRenderedResponse()
        {
            // Arrange
            var error = new ConflictError("a", "b");
            var options = new PresenterOptions();
            options.WithErrorRenderer<ConflictError>(err => new ObjectResult("customRender"));

            var sut = new Presenter(EmptyServiceProvider(), Options.Create(options),
                NullLogger<Presenter>.Instance);

            // Act
            var result = sut.Present(error);

            // Assert
            Assert.True(result is ObjectResult);
            Assert.Equal("customRender", ((ObjectResult)result).Value);
        }

        [Fact]
        public void Present_WithNullObject_RendersStatusCodeResponse()
        {
            // Arrange
            var sut = new Presenter(EmptyServiceProvider(), Options.Create(new PresenterOptions()),
                NullLogger<Presenter>.Instance);

            // Act
            var result = sut.Present((object)null, HttpStatusCode.Ambiguous);

            // Assert
            Assert.True(result is StatusCodeResult);
            Assert.Equal(300, ((StatusCodeResult)result).StatusCode);
        }

        [Fact]
        public void Present_WithErrorObject_RendersAsError()
        {
            // Arrange
            var error = new ConflictError("a", "b");
            var sut = new Presenter(EmptyServiceProvider(), Options.Create(new PresenterOptions()),
                NullLogger<Presenter>.Instance);

            // Act
            var result = sut.Present((object)error, HttpStatusCode.Ambiguous);

            // Assert
            Assert.True(result is ObjectResult);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public void Present_WithObjectAndNoContent_RendersAsNoContent()
        {
            // Arrange
            var sut = new Presenter(EmptyServiceProvider(), Options.Create(new PresenterOptions()),
                NullLogger<Presenter>.Instance);

            // Act
            var result = sut.Present(new object(), HttpStatusCode.NoContent);

            // Assert
            Assert.True(result is NoContentResult);
        }

        [Fact]
        public void Present_WithObject_RendersAsObjectResult()
        {
            // Arrange
            var value = new object();
            var sut = new Presenter(EmptyServiceProvider(), Options.Create(new PresenterOptions()),
                NullLogger<Presenter>.Instance);

            // Act
            var result = sut.Present(value, HttpStatusCode.AlreadyReported);

            // Assert
            Assert.True(result is ObjectResult);
            Assert.Equal(value, ((ObjectResult)result).Value);
            Assert.Equal(208, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public void Present_WithFailedResult_RendersError()
        {
            // Arrange
            var error = UnexpectedError.FromException(new NotImplementedException());

            var sut = new Presenter(EmptyServiceProvider(), Options.Create(new PresenterOptions()),
                NullLogger<Presenter>.Instance);

            // Act
            var result = sut.Present(Result.Failure(error), HttpStatusCode.Accepted);

            // Assert
            Assert.True(result is ObjectResult);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public void Present_WithSuccessfulResult_RendersValue()
        {
            // Arrange
            var value = new object();

            var sut = new Presenter(EmptyServiceProvider(), Options.Create(new PresenterOptions()),
                NullLogger<Presenter>.Instance);

            // Act
            var result = sut.Present(Result.Success(value), HttpStatusCode.Accepted);

            // Assert
            Assert.True(result is ObjectResult);
            Assert.Equal(202, ((ObjectResult)result).StatusCode);
            Assert.Equal(value, ((ObjectResult)result).Value);
        }

        [Fact]
        public void Present_WithFailedResultOfT_RendersError()
        {
            // Arrange
            var error = UnexpectedError.FromException(new NotImplementedException());

            var sut = new Presenter(EmptyServiceProvider(), Options.Create(new PresenterOptions()),
                NullLogger<Presenter>.Instance);

            // Act
            var result = sut.Present(new Result<string>(error), HttpStatusCode.Accepted);

            // Assert
            Assert.Equal(500, ((ObjectResult)result.Result).StatusCode);
        }

        [Fact]
        public void Present_WithSuccessfulResultOfT_RendersValue()
        {
            // Arrange
            var value = "teste";

            var sut = new Presenter(EmptyServiceProvider(), Options.Create(new PresenterOptions()),
                NullLogger<Presenter>.Instance);

            // Act
            var result = sut.Present(Result.Success(value), HttpStatusCode.Accepted);

            // Assert
            var objRes = result.Result as ObjectResult;
            Assert.Equal(202, objRes.StatusCode);
            Assert.Equal("teste", objRes.Value);
        }

        [Fact]
        public void PresentAs_WithFailedResult_RendersError()
        {
            // Arrange
            var error = UnexpectedError.FromException(new NotImplementedException());

            var sut = new Presenter(EmptyServiceProvider(), Options.Create(new PresenterOptions()),
                NullLogger<Presenter>.Instance);

            // Act
            var result = sut.PresentAs<int>(Result.Failure(error), HttpStatusCode.Accepted);

            // Assert
            Assert.Equal(500, ((ObjectResult)result.Result).StatusCode);
        }

        [Fact]
        public void PresentAs_WithResult_MapsThenRenders()
        {
            // Arrange
            var value = "teste";

            var mapper = new Mock<IMapper>(MockBehavior.Strict);
            mapper
                .Setup(x => x.Map<int>(value))
                .Returns(123);

            var provider = new ServiceCollection()
                .AddSingleton<IMapper>(mapper.Object)
                .BuildServiceProvider();

            var sut = new Presenter(provider, Options.Create(new PresenterOptions()),
                NullLogger<Presenter>.Instance);

            // Act
            var result = sut.PresentAs<int>(Result.Success(value), HttpStatusCode.Accepted);

            // Assert
            var objRes = result.Result as ObjectResult;
            Assert.Equal(202, objRes.StatusCode);
            Assert.Equal(123, objRes.Value);
        }

        [Fact]
        public void PresentAs_WithObject_MapsThenRenders()
        {
            // Arrange
            var value = "teste";

            var mapper = new Mock<IMapper>(MockBehavior.Strict);
            mapper
                .Setup(x => x.Map<int>(value))
                .Returns(123);

            var provider = new ServiceCollection()
                .AddSingleton<IMapper>(mapper.Object)
                .BuildServiceProvider();

            var sut = new Presenter(provider, Options.Create(new PresenterOptions()),
                NullLogger<Presenter>.Instance);

            // Act
            var result = sut.PresentAs<int>(value, HttpStatusCode.Accepted);

            // Assert
            var objRes = result.Result as ObjectResult;
            Assert.Equal(202, objRes.StatusCode);
            Assert.Equal(123, objRes.Value);
        }
    }
}
