using Xunit;

namespace Fusap.Common.Model.Tests
{
    public class ErrorCatalogEntryTests
    {
        [Fact]
        public void ImplicitOperatorOfTuple_WithTuple_ReturnsEntry()
        {
            // Arrange


            // Act
            ErrorCatalogEntry entry;
            entry = ("code", "message");

            // Assert
            Assert.Equal("code", entry.Code);
            Assert.Equal("message", entry.Message);
        }

        [Fact]
        public void ImplicitOperatorOfString_WithEntry_ReturnsCode()
        {
            // Arrange
            var entry = new ErrorCatalogEntry("code", "message");

            // Act
            var str = string.Empty;
            str = entry;

            // Assert
            Assert.Equal("code", str);
        }

        [Fact]
        public void ImplicitOperatorOfError_WithEntry_ReturnsError()
        {
            // Arrange
            var entry = new ErrorCatalogEntry("code", "message");

            // Act
            // ReSharper disable once JoinDeclarationAndInitializer
            Error error;
            error = entry;

            // Assert
            Assert.Equal("code", error.Code);
            Assert.Equal("message", error.Message);
        }

        [Fact]
        public void ToString_WithEntry_ReturnsCode()
        {
            // Arrange
            var entry = new ErrorCatalogEntry("code", "message");

            // Act
            var str = entry.ToString();

            // Assert
            Assert.Equal("code", str);
        }
    }
}
