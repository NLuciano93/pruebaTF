using System;
using Xunit;

namespace Fusap.Common.Model.Tests.Errors
{
    public class UnexpectedErrorTests
    {
        [Fact]
        public void ToString_WithCodeAndMessage_ProducesCorrectRepresentation()
        {
            // Arrange
            var error = new UnexpectedError("a", "b");

            // Act
            var str = error.ToString();

            // Assert
            Assert.Equal("a: b", str);
        }

        [Fact]
        public void ToString_WithException_ProducesCorrectRepresentation()
        {
            // Arrange
            UnexpectedError error;
            try
            {
                throw new InvalidCastException();
            }
            catch (Exception e)
            {
                error = UnexpectedError.FromException(e);
            }

            // Act
            var str = error.ToString();

            // Assert
            Assert.StartsWith(@"UNEXPECTED_ERROR: An unexpected error happened (System.InvalidCastException: Specified cast is not valid.
   at Fusap.Common.Model.Tests.Errors.UnexpectedErrorTests.ToString_WithException_ProducesCorrectRepresentation()", str);
        }
    }
}
