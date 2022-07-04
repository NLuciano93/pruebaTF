using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fusap.Common.Authorization.Client.Tests
{
    public class FusapAuthorizationClientTests
    {
        private readonly FusapAuthorizationClientOptions _authorizationClientOptions;

        public FusapAuthorizationClientTests()
        {
            _authorizationClientOptions = new FusapAuthorizationClientOptions
            {
                Authorities = new[]
                {
                    new ResourceAuthority
                    {
                        ConnectionString = new Uri("https://persons-accounts-auth"),
                        Resources = new[] {"urn:fusap:account:", "urn:fusap:person:"}
                    },
                    new ResourceAuthority
                    {
                        ConnectionString = new Uri("https://cards-auth"),
                        Resources = new[] {"urn:fusap:card:"}
                    }
                }
            };
        }

        [Fact]
        public async Task AuthorizeAsync_WithRequirementThatCrossAuthorityBoundaries_Throws()
        {
            // Arrange
            var httpHandler = new MockHttpMessageHandler();

            var requirements = new[]
            {
                new Requirement(new Uri("urn:fusap:person:1"), new [] { new Uri("urn:fusap:account:1"), new Uri("urn:fusap:card:1") }, "account:view-balance"),
            };

            var sut = new FusapAuthorizationClient(httpHandler.ToHttpClient(), Options.Create(_authorizationClientOptions), NullLogger<FusapAuthorizationClient>.Instance);

            // Act
            async Task action()
            {
                await sut.AuthorizeAsync(requirements, CancellationToken.None);
            }

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }

        [Fact]
        public async Task AuthorizeAsync_WithSingleAuthority_MakesOnlyOneHttpCallAndReturnsExpectedResult()
        {
            // Arrange
            var httpHandler = new MockHttpMessageHandler();

            var requirements = new[]
            {
                new Requirement(new Uri("urn:fusap:person:1"), new [] {new Uri("urn:fusap:account:1"), new Uri("urn:fusap:person:1")}, "account:view-balance"),
                new Requirement(new Uri("urn:fusap:person:2"), new [] {new Uri("urn:fusap:account:2")}, "account:view-balance"),
                new Requirement(new Uri("urn:fusap:person:2"), new Uri("urn:fusap:person:3"), "person:change-name", "person:change-birthday"),
            };

            httpHandler
                .When(HttpMethod.Post, "https://persons-accounts-auth")
                .WithContent("[{\"Identity\":\"urn:fusap:person:1\",\"Resources\":[\"urn:fusap:account:1\",\"urn:fusap:person:1\"],\"Actions\":[\"account:view-balance\"]},{\"Identity\":\"urn:fusap:person:2\",\"Resources\":[\"urn:fusap:account:2\"],\"Actions\":[\"account:view-balance\"]},{\"Identity\":\"urn:fusap:person:2\",\"Resources\":[\"urn:fusap:person:3\"],\"Actions\":[\"person:change-name\",\"person:change-birthday\"]}]")
                .Respond("application/json", "[true,false,true]")
                ;

            var sut = new FusapAuthorizationClient(httpHandler.ToHttpClient(), Options.Create(_authorizationClientOptions), NullLogger<FusapAuthorizationClient>.Instance);

            // Act
            var result = await sut.AuthorizeAsync(requirements, CancellationToken.None);

            // Assert
            Assert.False(result);
            Assert.True(result[requirements[0]]);
            Assert.False(result[requirements[1]]);
            Assert.True(result[requirements[2]]);
        }

        [Fact]
        public async Task AuthorizeAsync_WithTwoAuthorities_MakesTwoHttpCallsAndReturnsExpectedResult()
        {
            // Arrange
            var httpHandler = new MockHttpMessageHandler();

            var requirements = new[]
            {
                new Requirement(new Uri("urn:fusap:person:1"), new [] {new Uri("urn:fusap:account:1"), new Uri("urn:fusap:person:1")}, "account:view-balance"),
                new Requirement(new Uri("urn:fusap:person:2"), new Uri("urn:fusap:card:2"), "card:cancel"),
                new Requirement(new Uri("urn:fusap:person:2"), new [] {new Uri("urn:fusap:account:2")}, "account:view-balance"),
                new Requirement(new Uri("urn:fusap:person:2"), new Uri("urn:fusap:person:3"), "person:change-name", "person:change-birthday"),
                new Requirement(new Uri("urn:fusap:person:2"), new Uri("urn:fusap:card:4"), "card:change-pin"),
            };

            httpHandler
                .When(HttpMethod.Post, "https://persons-accounts-auth")
                .WithContent("[{\"Identity\":\"urn:fusap:person:1\",\"Resources\":[\"urn:fusap:account:1\",\"urn:fusap:person:1\"],\"Actions\":[\"account:view-balance\"]},{\"Identity\":\"urn:fusap:person:2\",\"Resources\":[\"urn:fusap:account:2\"],\"Actions\":[\"account:view-balance\"]},{\"Identity\":\"urn:fusap:person:2\",\"Resources\":[\"urn:fusap:person:3\"],\"Actions\":[\"person:change-name\",\"person:change-birthday\"]}]")
                .Respond("application/json", "[true,false,true]");

            httpHandler
                .When(HttpMethod.Post, "https://cards-auth")
                .WithContent("[{\"Identity\":\"urn:fusap:person:2\",\"Resources\":[\"urn:fusap:card:2\"],\"Actions\":[\"card:cancel\"]},{\"Identity\":\"urn:fusap:person:2\",\"Resources\":[\"urn:fusap:card:4\"],\"Actions\":[\"card:change-pin\"]}]")
                .Respond("application/json", "[false,true]");

            var sut = new FusapAuthorizationClient(httpHandler.ToHttpClient(), Options.Create(_authorizationClientOptions), NullLogger<FusapAuthorizationClient>.Instance);

            // Act
            var result = await sut.AuthorizeAsync(requirements, CancellationToken.None);

            // Assert
            Assert.False(result);
            Assert.True(result[requirements[0]]);
            Assert.False(result[requirements[1]]);
            Assert.False(result[requirements[2]]);
            Assert.True(result[requirements[3]]);
            Assert.True(result[requirements[4]]);
        }

    }
}