using Xunit;

namespace Fusap.Common.Model.Tests.Errors
{
    public class ErrorTests
    {
        [Fact]
        public void ToString_WithCodeAndMessage_ProducesCorrectRepresentation()
        {
            // Arrange
            var error = new Error("a", "b");

            // Act
            var str = error.ToString();

            // Assert
            Assert.Equal("a: b", str);
        }
    }
}
