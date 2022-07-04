using System;
using Xunit;

namespace Fusap.Common.Authorization.Client.Tests
{
    public class RequirementTests
    {
        [Fact]
        public void Constructor_WithSingleResource_ReturnsObject()
        {
            // Arrange
            var identity = new Uri("urn:a");
            var resource1 = new Uri("urn:b");
            var action1 = "test1";
            var action2 = "test2";

            // Act
            var sut = new Requirement(identity, resource1, action1, action2);

            // Assert
            Assert.Equal(identity, sut.Identity);
            Assert.Equal(new[] { resource1 }, sut.Resources);
            Assert.Equal(new[] { action1, action2 }, sut.Actions);
        }

        [Fact]
        public void Constructor_WithResourceArray_ReturnsObject()
        {
            // Arrange
            var identity = new Uri("urn:a");
            var resource1 = new Uri("urn:b");
            var resource2 = new Uri("urn:c");
            var action1 = "test1";
            var action2 = "test2";

            // Act
            var sut = new Requirement(identity, new[] { resource1, resource2 }, action1, action2);

            // Assert
            Assert.Equal(identity, sut.Identity);
            Assert.Equal(new[] { resource1, resource2 }, sut.Resources);
            Assert.Equal(new[] { action1, action2 }, sut.Actions);
        }

        [Fact]
        public void Constructor_WithNullIdentity_Throws()
        {
            // Arrange
            // Act
            static void Action()
            {
#pragma warning disable S1481 // Unused local variables should be removed
                var sut = new Requirement(null!, new Uri("urn:a"), "b");
#pragma warning restore S1481 // Unused local variables should be removed
            }

            // Assert
            Assert.Throws<ArgumentNullException>(Action);
        }

        [Fact]
        public void Constructor_WithSingleNullResource_Throws()
        {
            // Arrange
            // Act
            static void Action()
            {
#pragma warning disable S1481 // Unused local variables should be removed
                var sut = new Requirement(new Uri("urn:a"), ((Uri)null)!, "b");
#pragma warning restore S1481 // Unused local variables should be removed
            }

            // Assert
            Assert.Throws<ArgumentNullException>(Action);
        }

        [Fact]
        public void Constructor_WithNullResourceArray_Throws()
        {
            // Arrange
            // Act
            static void Action()
            {
#pragma warning disable S1481 // Unused local variables should be removed
                var sut = new Requirement(new Uri("urn:a"), ((Uri[])null)!, "b");
#pragma warning restore S1481 // Unused local variables should be removed
            }

            // Assert
            Assert.Throws<ArgumentNullException>(Action);
        }

        [Fact]
        public void Constructor_WithResourceArrayWithNullElement_Throws()
        {
            // Arrange
            // Act
            static void Action()
            {
#pragma warning disable S1481 // Unused local variables should be removed
                var sut = new Requirement(new Uri("urn:a"), new[] { new Uri("urn:b"), null }, "b");
#pragma warning restore S1481 // Unused local variables should be removed
            }

            // Assert
            Assert.Throws<ArgumentNullException>(Action);
        }

        [Fact]
        public void Constructor_WithNullActions_Throws()
        {
            // Arrange
            // Act
            static void Action()
            {
#pragma warning disable S1481 // Unused local variables should be removed
                var sut = new Requirement(new Uri("urn:a"), new Uri("urn:b"), null!);
#pragma warning restore S1481 // Unused local variables should be removed
            }

            // Assert
            Assert.Throws<ArgumentNullException>(Action);
        }

        [Fact]
        public void Constructor_WithResourceArrayAndNullActions_Throws()
        {
            // Arrange
            // Act
            static void Action()
            {
#pragma warning disable S1481 // Unused local variables should be removed
                var sut = new Requirement(new Uri("urn:a"), new[] { new Uri("urn:b") }, null!);
#pragma warning restore S1481 // Unused local variables should be removed
            }

            // Assert
            Assert.Throws<ArgumentNullException>(Action);
        }


        [Fact]
        public void ToString_WithIdentityResourceAndAction_ReturnsExpectedString()
        {
            // Arrange
            var identity = new Uri("urn:a");
            var resource1 = new Uri("urn:b");
            var resource2 = new Uri("urn:c");
            var action1 = "test1";
            var action2 = "test2";

            // Act
            var sut = new Requirement(identity, new[] { resource2, resource1 }, action1, action2);

            // Assert
            Assert.Equal("id=urn:a;res=urn:b,urn:c;act=test1,test2", sut.ToString());
        }
    }
}
