using System;
using Xunit;

namespace Fusap.Common.Model.Tests
{
    public class FusapModelExtensionsTests
    {
        [Fact]
        public void IsSuccessful_WithNull_Throws()
        {
            // Arrange

            // Act
            void Act()
            {
                ((IResult)null!).IsSuccessful();
            }

            // Assert
            Assert.Throws<ArgumentNullException>(Act);
        }

        [Fact]
        public void IsSuccessful_WithError_ReturnsFalse()
        {
            // Arrange
            var result = Result.Failure(new Error("1", "test"));

            // Act
            var resp = result.IsSuccessful();

            // Assert
            Assert.False(resp);
        }

        [Fact]
        public void IsSuccessful_Without_ReturnsTrue()
        {
            // Arrange
            var result = Result.Success();

            // Act
            var resp = result.IsSuccessful();

            // Assert
            Assert.True(resp);
        }

        [Fact]
        public void EnsureSuccess_WithNull_Throws()
        {
            // Arrange

            // Act
            void Act()
            {
                ((IResult)null!).EnsureSuccess();
            }

            // Assert
            Assert.Throws<ArgumentNullException>(Act);
        }

        [Fact]
        public void EnsureSuccess_WithError_Throws()
        {
            // Arrange
            var result = Result.Failure(new Error("1", "test"));

            // Act
            void Act()
            {
                result.EnsureSuccess();
            }

            // Assert
            Assert.Throws<FailedResultException>(Act);
        }

        [Fact]
        public void EnsureSuccess_Without_DoesNothing()
        {
            // Arrange
            var result = Result.Success();

            // Act
            result.EnsureSuccess();

            // Assert
            // No assertion, it just cannot throw.
        }

        [Fact]
        public void AsResult_WithResult_ReturnsSame()
        {
            // Arrange
            var result = Result.Success();

            // Act
            var final = result.AsResult();

            // Assert
            Assert.True(final.Equals(result));
        }

        [Fact]
        public void AsResult_WithCompatibleResultFailed_ReturnsSameError()
        {
            // Arrange
            var result = new CompatibleResult
            {
                Error = new Error("1", "test")
            };

            // Act
            var final = result.AsResult();

            // Assert
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.False(final.Equals(result));
            Assert.Equal(result.Error, final.Error);
        }

        [Fact]
        public void AsResult_WithCompatibleResultSuccess_ReturnsSameValue()
        {
            // Arrange
            var result = new CompatibleResult
            {
                Value = new object()
            };

            // Act
            var final = result.AsResult();

            // Assert
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.False(final.Equals(result));
            Assert.Equal(result.Value, final.Value);
        }

        [Fact]
        public void AsResult_WithResultOfT_ReturnsSame()
        {
            // Arrange
            var result = Result.Success<bool>(true);

            // Act
            var final = result.AsResult();

            // Assert
            Assert.True(final.Equals(result));
        }

        [Fact]
        public void AsResult_WithCompatibleResultOfTFailed_ReturnsSameError()
        {
            // Arrange
            var result = new CompatibleResult<bool>
            {
                Error = new Error("1", "test")
            };

            // Act
            var final = result.AsResult();

            // Assert
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.False(final.Equals(result));
            Assert.Equal(result.Error, final.Error);
        }

        [Fact]
        public void AsResult_WithCompatibleResultOfTSuccess_ReturnsSameValue()
        {
            // Arrange
            var result = new CompatibleResult<object>
            {
                Value = new object()
            };

            // Act
            var final = result.AsResult();

            // Assert
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.False(final.Equals(result));
            Assert.Equal(result.Value, final.Value);
        }
    }
}
