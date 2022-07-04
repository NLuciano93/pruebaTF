using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Fusap.Common.ApiClient.Abstractions.Tests
{
    public class ApiClientServiceExtensionsTests
    {
        [Fact]
        public void AddApiClient_WithoutConfiguration_ReturnsHttpClientBuilderAndRegisterServices()
        {
            // Arrange
            var sut = new ServiceCollection();

            // Act
            var builder = sut.AddApiClient<ITestClient, TestClient>();

            // Assert
            Assert.NotNull(builder);
            Assert.Contains(sut, d => d.ServiceType == typeof(ITestClient));
            Assert.Contains(sut, d =>
                d.ServiceType == typeof(IConfigureOptions<ApiClientOptions<ITestClient>>) &&
                d.ImplementationType == typeof(ApiClientConfigureOptions<ITestClient>));
        }

    }
}
