using Xunit;

namespace Fusap.Common.Model.Tests
{
    public class ErrorCatalogEntryExtensionsTests
    {
        [Fact]
        public void Format_WithOneParameter_ReturnsFormattedMessage()
        {
            // Arrange

            // Act
            var msg = ErrorCatalog.ErrorWithOneParameter.Format("one");

            // Assert
            Assert.Equal("RN-02", msg.Code);
            Assert.Equal("This error has one parameter and it is 'one'", msg.Message);
        }

        [Fact]
        public void Format_WithTwoParameter_ReturnsFormattedMessage()
        {
            // Arrange

            // Act
            var msg = ErrorCatalog.ErrorWithTwoParameter.Format("one", "two");

            // Assert
            Assert.Equal("RN-04", msg.Code);
            Assert.Equal("This error has two parameter and they are 'one' and 'two'", msg.Message);
        }

        [Fact]
        public void Format_WithThreeParameter_ReturnsFormattedMessage()
        {
            // Arrange

            // Act
            var msg = ErrorCatalog.ErrorWithThreeParameter.Format("one", "two", "three");

            // Assert
            Assert.Equal("RN-03", msg.Code);
            Assert.Equal("This error has three parameter and they are 'one', 'two' and 'three'", msg.Message);
        }

        [Fact]
        public void Format_WithFourParameter_ReturnsFormattedMessage()
        {
            // Arrange

            // Act
            var msg = ErrorCatalog.ErrorWithFourParameter.Format("one", "two", "three", "four");

            // Assert
            Assert.Equal("RN-05", msg.Code);
            Assert.Equal("This error has four parameter and they are 'one', 'two', 'three' and 'four'", msg.Message);
        }
    }
}
