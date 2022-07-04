using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace Fusap.Common.Model.Presenter.WebApi.Tests
{
    public class PresentResultFilterTests
    {
        [Fact]
        public void OnActionExecuted_WithObjectResultContainingResult_RendersAndSetsUpdatedResult()
        {
            // Arrange
            var expected = new OkObjectResult(null);

            var presenter = new Mock<IPresenter>(MockBehavior.Strict);
            presenter
                .Setup(x => x.Present(It.IsAny<IResult>(), HttpStatusCode.Ambiguous))
                .Returns(expected);

            var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
            var context = new ActionExecutedContext(actionContext, Array.Empty<IFilterMetadata>(), null)
            {
                Result = new ObjectResult(Result.Success("teste"))
                {
                    StatusCode = (int)HttpStatusCode.Ambiguous
                }
            };

            var sut = new PresentResultFilter(presenter.Object);

            // Act
            sut.OnActionExecuted(context);

            // Assert
            Assert.Equal(expected, context.Result);
            presenter.VerifyAll();
        }
    }
}
