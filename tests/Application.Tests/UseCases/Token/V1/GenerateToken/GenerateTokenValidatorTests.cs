using FluentValidation.TestHelper;
using Xunit;

namespace Fusap.TimeSheet.Application.UseCases.Token.V1.GenerateToken.Tests
{
    public class GenerateTokenValidatorTests
    {
        [Fact]
        public void Validate_WithValidFields_ReturnsNoValidationErrors()
        {
            // Arrange
            var generateTokenRequest = new GenerateTokenRequest()
            {
                Username = "amenapace",
                Password = "1234567"
            };

            var sut = new GenerateTokenValidator();

            // Act
            var result = sut.TestValidate(generateTokenRequest);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
