using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace Fusap.Common.Model.Presenter.WebApi.Tests
{
    public class PresentExceptionFilterTests
    {
        [Fact]
        public void OnException_WithException_RendersAndSetsUpdatedResult()
        {
            // Arrange
            var expected = new OkObjectResult(null);

            var presenter = new Mock<IPresenter>(MockBehavior.Strict);
            presenter
                .Setup(x => x.Present(It.IsAny<Error>()))
                .Returns(expected);

            var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
            var context = new ExceptionContext(actionContext, Array.Empty<IFilterMetadata>())
            {
                Exception = new NotImplementedException()
            };

            var sut = new PresentExceptionFilter(presenter.Object);

            // Act
            sut.OnException(context);

            // Assert
            Assert.Equal(expected, context.Result);
            presenter.VerifyAll();
        }
    }
}
