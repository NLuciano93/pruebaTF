using Xunit;

namespace Fusap.Common.Model.Tests
{
    public class PaginationOfTItemTests
    {
        [Fact]
        public void Constructor_WithItems_ReturnsPagination()
        {
            // Arrange

            // Act
            var pagination = new Pagination<string>(new[] { "a", "b" }, 10);

            // Assert
            Assert.Equal(10, pagination.Next);
            Assert.Contains("a", pagination.Items);
            Assert.Contains("b", pagination.Items);
            Assert.Null(pagination.EstimatedCount);
        }

        [Fact]
        public void Constructor_WithItemsAndEstimatedCount_ReturnsPagination()
        {
            // Arrange

            // Act
            var pagination = new Pagination<string>(new[] { "a", "b" }, 10, 200);

            // Assert
            Assert.Equal(10, pagination.Next);
            Assert.Contains("a", pagination.Items);
            Assert.Contains("b", pagination.Items);
            Assert.Equal(200, pagination.EstimatedCount);
        }

        [Fact]
        public void GetEnumerator_WithEnumerable_ReturnsEnumeratorOfSameType()
        {
            // Arrange
            var pagination = new Pagination<string>(new[] { "a", "b" }, 10);

            // Act
            using var enumerator = pagination.GetEnumerator();

            // Assert
            using var itemsEnumerator = pagination.Items.GetEnumerator();
            Assert.True(enumerator.GetType() == itemsEnumerator.GetType());
        }

        [Fact]
        public void Empty_WithNothing_ReturnsEmptyPagination()
        {
            // Arrange
            // Act
            var pagination = Pagination<string>.Empty();

            // Assert
            Assert.Empty(pagination.Items);
            Assert.Equal(default, pagination.Next);
            Assert.Null(pagination.EstimatedCount);
        }
    }
}
