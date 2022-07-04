using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;
// ReSharper disable JoinDeclarationAndInitializer

namespace Fusap.Common.Model.Tests
{
    public class ResultOfTValueTests
    {
        [Fact]
        public void Value_WithError_Throws()
        {
            // Arrange
            var sut = new Result<string>(new Error("01", "teste"));


            // Act
            object Act()
            {
                return sut.Value;
            }

            // Assert
            Assert.Throws<FailedResultException>(Act);
        }

        [Fact]
        public void Value_WithSuccess_ReturnsValue()
        {
            // Arrange
            var sut = new Result<string>("teste");

            // Act
            var ret = sut.Value;

            // Assert
            Assert.Equal("teste", ret);
        }

        [Fact]
        public void ImplicitOperatorOfTValue_WithResult_ReturnsValue()
        {
            // Arrange
            var result = Result.Success("test");

            // Act
            string value;
            value = result;

            // Assert
            Assert.Equal("test", value);
        }

        [Fact]
        public void ImplicitOperatorOfResultOfTValue_WithTValue_ReturnsResult()
        {
            // Arrange

            // Act
            Result<string> result;
            result = "test";

            // Assert
            Assert.Equal("test", result.Value);
        }

        [Fact]
        public void ImplicitOperatorOfResultOfTValue_WithError_ReturnsResult()
        {
            // Arrange
            var error = new Error("a", "b");

            // Act
            Result<string> result;
            result = error;

            // Assert
            Assert.Equal(error, result.Error);
        }

        [Fact]
        public void ImplicitOperatorOfResult_WithResultOfTValue_ReturnsResult()
        {
            // Arrange
            var result = Result.Success("test");

            // Act
            Result final;
            final = result;

            // Assert
            Assert.Equal("test", final.Value);
        }

        [Fact]
        public void ImplicitOperatorOfResultOfTValue_WithResultHoldingCorrectType_ReturnsResult()
        {
            // Arrange
            var original = new Result("test");

            // Act
            Result<string> result;
            result = original;

            // Assert
            Assert.Equal("test", result.Value);
        }

        [Fact]
        public void ImplicitOperatorOfResultOfTValue_WithResultHoldingNull_Throws()
        {
            // Arrange
            var original = new Result();

            // Act
            void Act()
            {
                Result<string> result;
                result = original;

                Debug.Assert(result == default);
            }

            // Assert
            Assert.Throws<ArgumentNullException>(Act);
        }

        [Fact]
        public void ImplicitOperatorOfResultOfTValue_WithResultHoldingWrongType_ThrowsInvalidCast()
        {
            // Arrange
            var original = new Result(123);

            // Act
            void Act()
            {
                Result<string> result;
#pragma warning disable S1854 // Unused assignments should be removed
#pragma warning disable 8604
                result = original;
#pragma warning restore 8604
#pragma warning restore S1854 // Unused assignments should be removed
            }

            // Assert
            Assert.Throws<InvalidCastException>(Act);
        }

        [Fact]
        public void ImplicitOperatorOfResultOfTValue_WithResultHoldingError_ReturnsResult()
        {
            // Arrange
            var error = new Error("a", "b");
            var original = new Result(error);

            // Act
            Result<string> result;
            result = original;

            // Assert
            Assert.Equal(error, result.Error);
        }

        [Fact]
        public void ImplicitOperatorOfResult_WithFailedResultOfTValue_ReturnsResult()
        {
            // Arrange
            var error = new Error("a", "b");
            var result = new Result<string>(error);

            // Act
            Result final;
            final = result;

            // Assert
            Assert.Equal(error, final.Error);
        }

        [Fact]
        public void ImplicitOperatorOfTaskOfResult_WithResult_ReturnsTask()
        {
            // Arrange
            var result = Result.Success("test");

            // Act
            Task<Result> task;
            task = result;

            // Assert
            Assert.Equal("test", task.Result.Value);
        }

        [Fact]
        public void ImplicitOperatorOfTaskOfResultOfTValue_WithResult_ReturnsTask()
        {
            // Arrange
            var result = Result.Success("test");

            // Act
            Task<Result<string>> task;
            task = result;

            // Assert
            Assert.Equal(task.Result, result);
        }

        [Fact]
        public void IResultValue_WithValue_ReturnsSameValue()
        {
            // Arrange
            var result = Result.Success("teste");

            // Act
            var ret = ((IResult)result).Value;

            // Assert
            Assert.Equal("teste", ret);
        }

        [Fact]
        public void InequalityOperator_WithSameSuccessfulResults_ReturnsFalse()
        {
            // Arrange
            var res1 = new Result<string>("a");
            var res2 = new Result<string>("a");

            // Act
            var equal = (res1 != res2);

            // Assert
            Assert.False(equal);
        }

        [Fact]
        public void InequalityOperator_WithSameErrorResults_ReturnsFalse()
        {
            // Arrange
            var error = new Error("a", "b");
            var res1 = new Result<string>(error);
            var res2 = new Result<string>(error);

            // Act
            var equal = (res1 != res2);

            // Assert
            Assert.False(equal);
        }

        [Fact]
        public void InequalityOperator_WithDifferentResults_ReturnsTrue()
        {
            // Arrange
            var res1 = Result.Success("a");
            var res2 = new Result<string>(new Error("a", "b"));

            // Act
            var equal = (res1 != res2);

            // Assert
            Assert.True(equal);
        }
    }
}
