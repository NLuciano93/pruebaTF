using Xunit;

namespace Fusap.Common.Model.Tests.Errors
{
    public class ValidationErrorTests
    {
        [Fact]
        public void Construtor_WithDetails_ProducesError()
        {
            // Arrange
            var details = new[]
            {
                new ValidationErrorDetail("propA", "code1", "message1"),
                new ValidationErrorDetail("propB", ErrorCatalog.ErrorWithZeroParameters),
                new ValidationErrorDetail(ErrorCatalog.Test),
                ErrorCatalog.ErrorWithOneParameter
            };

            // Act
            var error = new ValidationError(details);

            // Assert
            Assert.Contains(error.Details, x => x.Code == "code1" && x.PropertyName == "propA");
            Assert.Contains(error.Details, x => x.Code == ErrorCatalog.ErrorWithZeroParameters && x.PropertyName == "propB");
            Assert.Contains(error.Details, x => x.Code == ErrorCatalog.Test);
            Assert.Contains(error.Details, x => x.Code == ErrorCatalog.ErrorWithOneParameter);
        }

        [Fact]
        public void ToString_WithDetails_ProducesCorrectRepresentation()
        {
            // Arrange
            var details = new[]
            {
                new ValidationErrorDetail("propA", "code1", "message1"),
                new ValidationErrorDetail("propB", ErrorCatalog.ErrorWithZeroParameters),
                new ValidationErrorDetail(ErrorCatalog.Test),
                ErrorCatalog.ErrorWithOneParameter
            };

            var error = new ValidationError(details);

            // Act
            var str = error.ToString();

            // Assert
            Assert.Equal("VALIDATION_ERROR: One or more values were invalid (code1 on property propA - message1, " +
                         "RN-01 on property propB - This error has a static message, " +
                         "RN-06 - This error has four parameter and they are '{0}', '{1}', '{2}' and '{3}', " +
                         "RN-02 - This error has one parameter and it is '{0}')", str);
        }

        [Fact]
        public void ToString_WithNoDetails_ProducesCorrectRepresentation()
        {
            // Arrange
            var error = new ValidationError();

            // Act
            var str = error.ToString();

            // Assert
            Assert.Equal("VALIDATION_ERROR: One or more values were invalid (no details)", str);
        }
    }
}
