using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Xunit;

namespace Fusap.Common.Model.Presenter.WebApi.Tests
{
    public class FusapApiControllerTests
    {
        [Fact]
        public void PresenterGet_WithServiceProvider_ReturnsService()
        {
            // Arrange
            var config = new ConfigurationBuilder().Build();
            var services = new ServiceCollection()
                .AddSingleton<IConfiguration>(config)
                .AddSingleton<IHostEnvironment>(new HostingEnvironment())
                .AddLogging()
                .AddFusapPresenter();

            var provider = services.BuildServiceProvider();
            var httpContext = new DefaultHttpContext
            {
                RequestServices = provider
            };
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var sut = new TestController { ControllerContext = controllerContext };

            // Act
            var presenter = sut.GetPresenter();

            // Assert
            Assert.NotNull(presenter);
        }
    }
}
