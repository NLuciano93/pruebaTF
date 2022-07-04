using System;
using Xunit;

namespace Fusap.Common.Authorization.Client.Tests
{
    public class AuthorizationResultTests
    {
        [Fact]
        public void Constructor_WithResults_ReturnsObject()
        {
            // Arrange
            var requirement1 = new Requirement(new Uri("urn:a"), new Uri("urn:b"), "action");
            var requirement2 = new Requirement(new Uri("urn:a"), new Uri("urn:c"), "action");
            var requirement3 = new Requirement(new Uri("urn:a"), new Uri("urn:d"), "action");
            var input = new[]
            {
                requirement1.Grant(),
                requirement2.Deny(),
                requirement3.Grant()
            };

            // Act
            var sut = new AuthorizationResult(input);

            // Assert
            Assert.Equal(input, sut);
            Assert.Equal(input[0], sut[requirement1]);
            Assert.Equal(input[1], sut[requirement2]);
            Assert.Equal(input[2], sut[requirement3]);
        }

        [Fact]
        public void Successful_WithNoResults_ReturnsFalse()
        {
            // Arrange
            // Act
            var sut = new AuthorizationResult(Array.Empty<RequirementResult>());

            // Assert
            Assert.False(sut.Successful);
        }

        [Fact]
        public void Successful_WithSomeUnsuccessfulResults_ReturnsFalse()
        {
            // Arrange
            var input = new[]
            {
                new RequirementResult(new Requirement(new Uri("urn:a"), new Uri("urn:b"), "action"), true),
                new RequirementResult(new Requirement(new Uri("urn:a"), new Uri("urn:c"), "action"), false),
                new RequirementResult(new Requirement(new Uri("urn:a"), new Uri("urn:d"), "action"), true),
            };

            // Act
            var sut = new AuthorizationResult(input);

            // Assert
            Assert.False(sut.Successful);
        }

        [Fact]
        public void Successful_WithAllSuccessfulResults_ReturnsTrue()
        {
            // Arrange
            var input = new[]
            {
                new RequirementResult(new Requirement(new Uri("urn:a"), new Uri("urn:b"), "action"), true),
                new RequirementResult(new Requirement(new Uri("urn:a"), new Uri("urn:c"), "action"), true),
                new RequirementResult(new Requirement(new Uri("urn:a"), new Uri("urn:d"), "action"), true),
            };

            // Act
            var sut = new AuthorizationResult(input);

            // Assert
            Assert.True(sut.Successful);
        }

        [Fact]
        public void IndexAccessor_WithEqualRequirement_ReturnsResult()
        {
            // Arrange
            var requirement1 = new Requirement(new Uri("urn:a"), new Uri("urn:b"), "action");
            var requirement2 = new Requirement(new Uri("urn:a"), new Uri("urn:b"), "action");
            var sut = new AuthorizationResult(requirement1.Grant());

            // Act
            var result = sut[requirement2];

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ImplicitBoolOperator_WithUnsuccessfulResults_ReturnsFalse()
        {
            // Arrange
            var input = new[]
            {
                new RequirementResult(new Requirement(new Uri("urn:a"), new Uri("urn:b"), "action"), false),
            };

            // Act
            var sut = new AuthorizationResult(input);

            // Assert
            Assert.False(sut);
        }

        [Fact]
        public void ImplicitBoolOperator_WithSuccessfulResults_ReturnsTrue()
        {
            // Arrange
            var input = new Requirement(new Uri("urn:a"), new Uri("urn:b"), "action").Grant();

            // Act
            var sut = new AuthorizationResult(input);

            // Assert
            Assert.True(sut);
        }
    }
}