using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Fusap.Common.ApiClient.Abstractions.Tests
{
    public class TestClientTests
    {
        [Fact]
        public void GetService_WithNoConnectionString_Throws()
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            services.AddApiClient<ITestClient, TestClient>();

            var sut = services.BuildServiceProvider();

            // Act
            void Act()
            {
                sut.GetService<ITestClient>();
            }

            // Assert
            Assert.Throws<ArgumentNullException>(Act);
        }

        [Fact]
        public void GetService_WithDefaultOptions_ReturnsConfiguredClient()
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    { "ConnectionStrings:Fusap.Common.ApiClient.Abstractions.Tests.ITestClient", "http://test123/" }
                })
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            services.AddApiClient<ITestClient, TestClient>();

            var sut = services.BuildServiceProvider();

            // Act
            var client = sut.GetService<ITestClient>();

            // Assert
            Assert.Equal("http://test123/", client.HttpClient.BaseAddress.ToString());
        }

        [Fact]
        public void GetService_WithCustomUrlAndAuthProvider_ReturnsConfiguredClient()
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    { "ConnectionStrings:Fusap.Common.ApiClient.Abstractions.Tests.ITestClient", "http://test123/" }
                })
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            services.AddApiClient<ITestClient, TestClient>(opt => opt
                .WithBaseUrl("http://test2/")
                .WithAuthenticationHeaderProvider(provider => new AuthenticationHeaderValue("Bearer", "bearerTest"))
            );

            var sut = services.BuildServiceProvider();

            // Act
            var client = sut.GetService<ITestClient>();

            // Assert
            Assert.Equal("http://test2/", client.HttpClient.BaseAddress.ToString());
            Assert.Equal("bearerTest", client.HttpClient.DefaultRequestHeaders.Authorization.Parameter);
        }
    }
}
