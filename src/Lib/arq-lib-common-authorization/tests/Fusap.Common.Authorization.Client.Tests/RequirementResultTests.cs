using System;
using Xunit;

namespace Fusap.Common.Authorization.Client.Tests
{
    public class RequirementResultTests
    {
        [Fact]
        public void Constructor_WithArguments_ReturnsObject()
        {
            // Arrange
            var req = new Requirement(new Uri("urn:a"), new Uri("urn:b"), "c");

            // Act
            var sut = new RequirementResult(req, true);

            // Assert
            Assert.Equal(req, sut.Requirement);
            Assert.True(sut.Successful);
        }

        [Fact]
        public void Constructor_WithoutRequirement_Throws()
        {
            // Arrange
            // Act
            static void Action()
            {
#pragma warning disable S1481 // Unused local variables should be removed
                var sut = new RequirementResult(null!, true);
#pragma warning restore S1481 // Unused local variables should be removed
            }

            // Assert
            Assert.Throws<ArgumentNullException>(Action);
        }

        [Fact]
        public void ImplicitBoolOperator_WithSuccess_ReturnsTrue()
        {
            // Arrange
            var req = new Requirement(new Uri("urn:a"), new Uri("urn:b"), "c");
            var sut = new RequirementResult(req, true);

            // Act
            bool result = sut;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ImplicitBoolOperator_WithoutSuccess_ReturnsFalse()
        {
            // Arrange
            var req = new Requirement(new Uri("urn:a"), new Uri("urn:b"), "c");
            var sut = new RequirementResult(req, false);

            // Act
            bool result = sut;

            // Assert
            Assert.False(result);
        }
    }
}
