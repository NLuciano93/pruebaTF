using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace Fusap.Common.Model.Tests
{
    public class ResultTests
    {
        [Fact]
        public void Constructor_WithNoArguments_RepresentsSuccess()
        {
            // Arrange

            // Act
            var result = new Result();

            // Assert
            Assert.Null(result.Error);
            Assert.Null(result.Value);
        }

        [Fact]
        public void Success_WithNoArguments_ReturnsResult()
        {
            // Arrange

            // Act
            var result = Result.Success();

            // Assert
            Assert.Null(result.Error);
            Assert.Null(result.Value);
        }

        [Fact]
        public void Success_WithValue_ReturnsResultWithValue()
        {
            // Arrange
            var value = new object();

            // Act
            var result = Result.Success(value);

            // Assert
            Assert.Null(result.Error);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void Success_WithTypedValue_ReturnsResultTypedValue()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = Result.Success(id);

            // Assert
            Assert.Equal(typeof(Result<Guid>), result.GetType());
            Assert.Equal(id, result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Failure_WithError_ReturnsFailedResult()
        {
            // Arrange
            var error = new Error(ErrorCatalog.Test);

            // Act
            var result = Result.Failure(error);

            // Assert
            Assert.Equal(error, result.Error);
        }

        [Fact]
        public void Value_WithError_Throws()
        {
            // Arrange
            var error = new Error("01", "teste");
            var sut = new Result(error);

            // Act
            object? Act()
            {
                return sut.Value;
            }

            // Assert
            var exception = Assert.Throws<FailedResultException>(Act);
            Assert.Equal(error, exception.Error);
        }

        [Fact]
        public void Value_WithSuccess_ReturnsValue()
        {
            // Arrange
            var sut = new Result("teste");

            // Act
            var ret = sut.Value;

            // Assert
            Assert.Equal("teste", ret);
        }

        [Fact]
        public void ImplicitOperatorOfResult_WithException_ReturnsFailedResultWithUnexpectedError()
        {
            // Arrange
            var exception = new NotImplementedException("test");
            Result MakeError()
            {
                try
                {
                    throw exception;
                }
                catch (Exception e)
                {
                    return e;
                }
            }

            // Act
            var error = MakeError();

            // Assert
            var unexpectedError = error.Error as UnexpectedError<NotImplementedException>;

            Assert.NotNull(unexpectedError);

            Debug.Assert(unexpectedError != null, nameof(unexpectedError) + " != null");
            Assert.Equal(exception, unexpectedError.Exception);
            Assert.Equal(exception, ((UnexpectedError)unexpectedError).Exception);
        }

        [Fact]
        public void ImplicitOperatorTaskResult_WithResult_ReturnsTaskCompleted()
        {
            // Arrange
            var result = Result.Failure(new Error("1", "a"));

            // Act
            // ReSharper disable once JoinDeclarationAndInitializer
            Task<Result> task;
            task = result;

            // Assert
            Assert.True(task.IsCompletedSuccessfully);
            Assert.Equal(result, task.Result);
        }

        [Fact]
        public void InequalityOperator_WithSameSuccessfulResults_ReturnsFalse()
        {
            // Arrange
            var res1 = new Result("a");
            var res2 = new Result("a");

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
            var res1 = new Result(error);
            var res2 = new Result(error);

            // Act
            var equal = (res1 != res2);

            // Assert
            Assert.False(equal);
        }

        [Fact]
        public void InequalityOperator_WithDifferentResults_ReturnsTrue()
        {
            // Arrange
            var res1 = new Result("a");
            var res2 = new Result(new Error("a", "b"));

            // Act
            var equal = (res1 != res2);

            // Assert
            Assert.True(equal);
        }
    }
}
